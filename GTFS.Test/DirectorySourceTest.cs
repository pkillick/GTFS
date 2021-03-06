﻿// The MIT License (MIT)

// Copyright (c) 2014 Ben Abelshausen

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using GTFS.Entities;
using GTFS.Entities.Enumerations;
using GTFS.IO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GTFS.Test
{
    /// <summary>
    /// Contains tests for the directory source.
    /// </summary>
    [TestFixture]
    public class DirectorySourceTest
    {
        /// <summary>
        /// Builds the source from embedded streams.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IGTFSSourceFile> BuildSource()
        {
            return new GTFSDirectorySource(new DirectoryInfo("sample-feed"));
        }

        /// <summary>
        /// Tests parsing agencies.
        /// </summary>
        [Test]
        public void ParseAgencies()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("agency")));

            // test result.
            Assert.IsNotNull(feed.Agencies);
            Assert.AreEqual(1, feed.Agencies.Count);
            Assert.AreEqual(null, feed.Agencies[0].FareURL);
            Assert.AreEqual("DTA", feed.Agencies[0].Id);
            Assert.AreEqual(null, feed.Agencies[0].LanguageCode);
            Assert.AreEqual("Demo Transit Authority", feed.Agencies[0].Name);
            Assert.AreEqual(null, feed.Agencies[0].Phone);
            Assert.AreEqual("America/Los_Angeles", feed.Agencies[0].Timezone);
            Assert.AreEqual("http://google.com", feed.Agencies[0].URL);
        }

        /// <summary>
        /// Tests parsing routes.
        /// </summary>
        [Test]
        public void ParseRoutes()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("routes")));

            // test result.
            Assert.IsNotNull(feed.Routes);
            Assert.AreEqual(5, feed.Routes.Count);

            //route_id,agency_id,route_short_name,route_long_name,route_desc,route_type,route_url,route_color,route_text_color

            //AB,DTA,10,Airport - Bullfrog,,3,,,
            int idx = 0;
            Assert.AreEqual("AB", feed.Routes[idx].Id);
            Assert.AreEqual("DTA", feed.Routes[idx].AgencyId);
            Assert.AreEqual("10", feed.Routes[idx].ShortName);
            Assert.AreEqual("Airport - Bullfrog", feed.Routes[idx].LongName);
            Assert.AreEqual(string.Empty, feed.Routes[idx].Description);
            Assert.AreEqual(RouteType.Bus, feed.Routes[idx].Type);
            Assert.AreEqual(-3932017, feed.Routes[idx].Color);
            Assert.AreEqual(null, feed.Routes[idx].TextColor);

            //BFC,DTA,20,Bullfrog - Furnace Creek Resort,,3,,,
            idx = 1;
            Assert.AreEqual("BFC", feed.Routes[idx].Id);
            Assert.AreEqual("DTA", feed.Routes[idx].AgencyId);
            Assert.AreEqual("20", feed.Routes[idx].ShortName);
            Assert.AreEqual("Bullfrog - Furnace Creek Resort", feed.Routes[idx].LongName);
            Assert.AreEqual(string.Empty, feed.Routes[idx].Description);
            Assert.AreEqual(RouteType.Bus, feed.Routes[idx].Type);
            Assert.AreEqual(-1, feed.Routes[idx].Color);
            Assert.AreEqual(null, feed.Routes[idx].TextColor);

            //STBA,DTA,30,Stagecoach - Airport Shuttle,,3,,,
            idx = 2;
            Assert.AreEqual("STBA", feed.Routes[idx].Id);
            Assert.AreEqual("DTA", feed.Routes[idx].AgencyId);
            Assert.AreEqual("30", feed.Routes[idx].ShortName);
            Assert.AreEqual("Stagecoach - Airport Shuttle", feed.Routes[idx].LongName);
            Assert.AreEqual(string.Empty, feed.Routes[idx].Description);
            Assert.AreEqual(RouteType.Bus, feed.Routes[idx].Type);
            Assert.AreEqual(null, feed.Routes[idx].Color);
            Assert.AreEqual(null, feed.Routes[idx].TextColor);

            //CITY,DTA,40,City,,3,,,
            idx = 3;
            Assert.AreEqual("CITY", feed.Routes[idx].Id);
            Assert.AreEqual("DTA", feed.Routes[idx].AgencyId);
            Assert.AreEqual("40", feed.Routes[idx].ShortName);
            Assert.AreEqual("City", feed.Routes[idx].LongName);
            Assert.AreEqual(string.Empty, feed.Routes[idx].Description);
            Assert.AreEqual(RouteType.Bus, feed.Routes[idx].Type);
            Assert.AreEqual(null, feed.Routes[idx].Color);
            Assert.AreEqual(null, feed.Routes[idx].TextColor);

            //AAMV,DTA,50,Airport - Amargosa Valley,,3,,,
            idx = 4;
            Assert.AreEqual("AAMV", feed.Routes[idx].Id);
            Assert.AreEqual("DTA", feed.Routes[idx].AgencyId);
            Assert.AreEqual("50", feed.Routes[idx].ShortName);
            Assert.AreEqual("Airport - Amargosa Valley", feed.Routes[idx].LongName);
            Assert.AreEqual(string.Empty, feed.Routes[idx].Description);
            Assert.AreEqual(RouteType.Bus, feed.Routes[idx].Type);
            Assert.AreEqual(null, feed.Routes[idx].Color);
            Assert.AreEqual(null, feed.Routes[idx].TextColor);
        }

        /// <summary>
        /// Tests parsing shapes.
        /// </summary>
        [Test]
        public void ParseShapes()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("shapes")));

            // test result.
            Assert.IsNotNull(feed.Shapes);
            Assert.AreEqual(44, feed.Shapes.Count);

            // @ 1: shape_id,shape_pt_lat,shape_pt_lon,shape_pt_sequence,shape_dist_traveled
            // @ 2: shape_1,37.754211,-122.197868,1,
            int idx = 0;
            Assert.AreEqual("shape_1", feed.Shapes[idx].Id);
            Assert.AreEqual(37.754211, feed.Shapes[idx].Latitude);
            Assert.AreEqual(-122.197868, feed.Shapes[idx].Longitude);
            Assert.AreEqual(1, feed.Shapes[idx].Sequence);
            Assert.AreEqual(null, feed.Shapes[idx].DistanceTravelled);

            // @ 10: shape_3,37.73645,-122.19706,1,
            idx = 8;
            Assert.AreEqual("shape_3", feed.Shapes[idx].Id);
            Assert.AreEqual(37.73645, feed.Shapes[idx].Latitude);
            Assert.AreEqual(-122.19706, feed.Shapes[idx].Longitude);
            Assert.AreEqual(1, feed.Shapes[idx].Sequence);
            Assert.AreEqual(null, feed.Shapes[idx].DistanceTravelled);
        }

        /// <summary>
        /// Tests parsing trips.
        /// </summary>
        [Test]
        public void ParseTrips()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>(false);

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("trips")));

            // test result.
            Assert.IsNotNull(feed.Trips);
            Assert.AreEqual(11, feed.Trips.Count);

            // @ 1: route_id,service_id,trip_id,trip_headsign,direction_id,block_id,shape_id
            // @ 2: AB,FULLW,AB1,to Bullfrog,0,1,shape_1
            int idx = 0;
            Assert.AreEqual("AB", feed.Trips[idx].RouteId);
            Assert.AreEqual("FULLW", feed.Trips[idx].ServiceId);
            Assert.AreEqual("AB1", feed.Trips[idx].Id);
            Assert.AreEqual("to Bullfrog", feed.Trips[idx].Headsign);
            Assert.AreEqual(DirectionType.OneDirection, feed.Trips[idx].Direction);
            Assert.AreEqual("1", feed.Trips[idx].BlockId);
            Assert.AreEqual("shape_1", feed.Trips[idx].ShapeId);

            // @ 10: BFC,FULLW,BFC1,to Furnace Creek Resort,0,1,shape_6
            idx = 5;
            Assert.AreEqual("BFC", feed.Trips[idx].RouteId);
            Assert.AreEqual("FULLW", feed.Trips[idx].ServiceId);
            Assert.AreEqual("BFC1", feed.Trips[idx].Id);
            Assert.AreEqual("to Furnace Creek Resort", feed.Trips[idx].Headsign);
            Assert.AreEqual(DirectionType.OneDirection, feed.Trips[idx].Direction);
            Assert.AreEqual("1", feed.Trips[idx].BlockId);
            Assert.AreEqual("shape_6", feed.Trips[idx].ShapeId);
        }

        /// <summary>
        /// Tests parsing stops.
        /// </summary>
        [Test]
        public void ParseStops()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("stops")));

            // test result.
            Assert.IsNotNull(feed.Stops);
            Assert.AreEqual(9, feed.Stops.Count);

            // @ 1: stop_id,stop_name,stop_desc,stop_lat,stop_lon,zone_id,stop_url
            // @ 2: FUR_CREEK_RES,Furnace Creek Resort (Demo),,36.425288,-117.133162,,
            int idx = 0;
            Assert.AreEqual("FUR_CREEK_RES", feed.Stops[idx].Id);
            Assert.AreEqual("Furnace Creek Resort (Demo)", feed.Stops[idx].Name);
            Assert.AreEqual(string.Empty, feed.Stops[idx].Description);
            Assert.AreEqual(36.425288, feed.Stops[idx].Latitude);
            Assert.AreEqual(-117.133162, feed.Stops[idx].Longitude);
            Assert.AreEqual(string.Empty, feed.Stops[idx].Url);

            // @ 10: AMV,Amargosa Valley (Demo),,36.641496,-116.40094,,
            idx = 8;
            Assert.AreEqual("AMV", feed.Stops[idx].Id);
            Assert.AreEqual("Amargosa Valley (Demo)", feed.Stops[idx].Name);
            Assert.AreEqual(string.Empty, feed.Stops[idx].Description);
            Assert.AreEqual(36.641496, feed.Stops[idx].Latitude);
            Assert.AreEqual(-116.40094, feed.Stops[idx].Longitude);
            Assert.AreEqual(string.Empty, feed.Stops[idx].Url);
        }

        /// <summary>
        /// Tests parsing stops.
        /// </summary>
        [Test]
        public void ParseStopTimes()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>(false);

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("stop_times")));

            // test result.
            Assert.IsNotNull(feed.StopTimes);
            Assert.AreEqual(28, feed.StopTimes.Count);

            // @ 1: trip_id,arrival_time,departure_time,stop_id,stop_sequence,stop_headsign,pickup_type,drop_off_time,shape_dist_traveled
            // @ 2: STBA,6:00:00,6:00:00,STAGECOACH,1,,,,
            int idx = 0;
            Assert.AreEqual("STBA", feed.StopTimes[idx].TripId);
            Assert.AreEqual(new TimeOfDay() { Hours = 6 }, feed.StopTimes[idx].ArrivalTime);
            Assert.AreEqual(new TimeOfDay() { Hours = 6 }, feed.StopTimes[idx].DepartureTime);
            Assert.AreEqual("STAGECOACH", feed.StopTimes[idx].StopId);
            Assert.AreEqual(1, feed.StopTimes[idx].StopSequence);
            Assert.AreEqual(string.Empty, feed.StopTimes[idx].StopHeadsign);
            Assert.AreEqual(null, feed.StopTimes[idx].PickupType);
            Assert.AreEqual(null, feed.StopTimes[idx].DropOffType);
            Assert.AreEqual(string.Empty, feed.StopTimes[idx].ShapeDistTravelled);

            // @ 12: CITY2,6:49:00,6:51:00,NANAA,4,,,,
            idx = 10;
            Assert.AreEqual("CITY2", feed.StopTimes[idx].TripId);
            Assert.AreEqual(new TimeOfDay() { Hours = 6, Minutes = 49 }, feed.StopTimes[idx].ArrivalTime);
            Assert.AreEqual(new TimeOfDay() { Hours = 6, Minutes = 51 }, feed.StopTimes[idx].DepartureTime);
            Assert.AreEqual("NANAA", feed.StopTimes[idx].StopId);
            Assert.AreEqual(4, feed.StopTimes[idx].StopSequence);
            Assert.AreEqual(string.Empty, feed.StopTimes[idx].StopHeadsign);
            Assert.AreEqual(null, feed.StopTimes[idx].PickupType);
            Assert.AreEqual(null, feed.StopTimes[idx].DropOffType);
            Assert.AreEqual(string.Empty, feed.StopTimes[idx].ShapeDistTravelled);
        }

        /// <summary>
        /// Tests parsing frequencies.
        /// </summary>
        [Test]
        public void ParseFrequencies()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>(false);

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("frequencies")));

            // test result.
            Assert.IsNotNull(feed.Frequencies);
            Assert.AreEqual(11, feed.Frequencies.Count);

            // @ 1: trip_id,start_time,end_time,headway_secs
            // @ 2: STBA,6:00:00,22:00:00,1800

            // @ 1: route_id,service_id,trip_id,trip_headsign,direction_id,block_id,shape_id
            // @ 2: AB,FULLW,AB1,to Bullfrog,0,1,shape_1
            int idx = 0;
            Assert.AreEqual("STBA", feed.Frequencies[idx].TripId);
            Assert.AreEqual("6:00:00", feed.Frequencies[idx].StartTime);
            Assert.AreEqual("22:00:00", feed.Frequencies[idx].EndTime);
            Assert.AreEqual("1800", feed.Frequencies[idx].HeadwaySecs);
            Assert.AreEqual(null, feed.Frequencies[idx].ExactTimes);

            // @ 10: CITY2,16:00:00,18:59:59,600
            idx = 8;
            Assert.AreEqual("CITY2", feed.Frequencies[idx].TripId);
            Assert.AreEqual("16:00:00", feed.Frequencies[idx].StartTime);
            Assert.AreEqual("18:59:59", feed.Frequencies[idx].EndTime);
            Assert.AreEqual("600", feed.Frequencies[idx].HeadwaySecs);
            Assert.AreEqual(null, feed.Frequencies[idx].ExactTimes);
        }

        /// <summary>
        /// Tests parsing calendars.
        /// </summary>
        [Test]
        public void ParseCalendars()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("calendar")));

            // test result.
            Assert.IsNotNull(feed.Calendars);
            Assert.AreEqual(2, feed.Calendars.Count);

            // @ 1: service_id,monday,tuesday,wednesday,thursday,friday,saturday,sunday,start_date,end_date
            // @ 2: FULLW,1,1,1,1,1,1,1,20070101,20101231
            int idx = 0;
            Assert.AreEqual("FULLW", feed.Calendars[idx].ServiceId);
            Assert.AreEqual(true, feed.Calendars[idx].Monday);
            Assert.AreEqual(true, feed.Calendars[idx].Tuesday);
            Assert.AreEqual(true, feed.Calendars[idx].Wednesday);
            Assert.AreEqual(true, feed.Calendars[idx].Thursday);
            Assert.AreEqual(true, feed.Calendars[idx].Friday);
            Assert.AreEqual(true, feed.Calendars[idx].Saturday);
            Assert.AreEqual(true, feed.Calendars[idx].Sunday);
            Assert.AreEqual(new DateTime(2007, 01, 01), feed.Calendars[idx].StartDate);
            Assert.AreEqual(new DateTime(2010, 12, 31), feed.Calendars[idx].EndDate);

            // @3: WE,0,0,0,0,0,1,1,20070101,20101231
            idx = 1;
            Assert.AreEqual("WE", feed.Calendars[idx].ServiceId);
            Assert.AreEqual(false, feed.Calendars[idx].Monday);
            Assert.AreEqual(false, feed.Calendars[idx].Tuesday);
            Assert.AreEqual(false, feed.Calendars[idx].Wednesday);
            Assert.AreEqual(false, feed.Calendars[idx].Thursday);
            Assert.AreEqual(false, feed.Calendars[idx].Friday);
            Assert.AreEqual(true, feed.Calendars[idx].Saturday);
            Assert.AreEqual(true, feed.Calendars[idx].Sunday);
            Assert.AreEqual(new DateTime(2007, 01, 01), feed.Calendars[idx].StartDate);
            Assert.AreEqual(new DateTime(2010, 12, 31), feed.Calendars[idx].EndDate);
        }

        /// <summary>
        /// Tests parsing calendar dates.
        /// </summary>
        [Test]
        public void ParseCalendarDates()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("calendar_dates")));

            // test result.
            Assert.IsNotNull(feed.CalendarDates);
            Assert.AreEqual(1, feed.CalendarDates.Count);

            // @ 1: service_id,date,exception_type
            // @ 2: FULLW,20070604,2
            int idx = 0;
            Assert.AreEqual("FULLW", feed.CalendarDates[idx].ServiceId);
            Assert.AreEqual(new System.DateTime(2007, 06, 04), feed.CalendarDates[idx].Date);
            Assert.AreEqual(ExceptionType.Removed, feed.CalendarDates[idx].ExceptionType);
        }

        /// <summary>
        /// Tests parsing routes.
        /// </summary>
        [Test]
        public void ParseFareRules()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("fare_rules")));

            // test result.
            Assert.IsNotNull(feed.FareRules);
            Assert.AreEqual(4, feed.FareRules.Count);

            // fare_id,route_id,origin_id,destination_id,contains_id

            //p,AB,,,
            int idx = 0;
            Assert.AreEqual("AB", feed.FareRules[idx].RouteId);
            Assert.AreEqual("p", feed.FareRules[idx].FareId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].OriginId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].DestinationId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].ContainsId);

            //p,STBA,,,
            idx = 1;
            Assert.AreEqual("STBA", feed.FareRules[idx].RouteId);
            Assert.AreEqual("p", feed.FareRules[idx].FareId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].OriginId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].DestinationId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].ContainsId);

            //p,BFC,,,
            idx = 2;
            Assert.AreEqual("BFC", feed.FareRules[idx].RouteId);
            Assert.AreEqual("p", feed.FareRules[idx].FareId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].OriginId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].DestinationId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].ContainsId);

            //a,AAMV,,,
            idx = 3;
            Assert.AreEqual("AAMV", feed.FareRules[idx].RouteId);
            Assert.AreEqual("a", feed.FareRules[idx].FareId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].OriginId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].DestinationId);
            Assert.AreEqual(string.Empty, feed.FareRules[idx].ContainsId);
        }

        /// <summary>
        /// Tests parsing routes.
        /// </summary>
        [Test]
        public void ParseFareAttributes()
        {
            // create the reader.
            GTFSReader<GTFSFeed> reader = new GTFSReader<GTFSFeed>();

            // build the source
            var source = this.BuildSource();

            // execute the reader.
            var feed = reader.Read(source, source.First(x => x.Name.Equals("fare_attributes")));

            // test result.
            Assert.IsNotNull(feed.FareAttributes);
            Assert.AreEqual(2, feed.FareAttributes.Count);

            //fare_id,price,currency_type,payment_method,transfers,transfer_duration

            //p,1.25,USD,0,0,
            int idx = 0;
            Assert.AreEqual("p", feed.FareAttributes[idx].FareId);
            Assert.AreEqual("1.25", feed.FareAttributes[idx].Price);
            Assert.AreEqual("USD", feed.FareAttributes[idx].CurrencyType);
            Assert.AreEqual(PaymentMethodType.OnBoard, feed.FareAttributes[idx].PaymentMethod);
            Assert.AreEqual(0, feed.FareAttributes[idx].Transfers);
            Assert.AreEqual(string.Empty, feed.FareAttributes[idx].TransferDuration);

            //a,5.25,USD,0,0,
            idx = 1;
            Assert.AreEqual("a", feed.FareAttributes[idx].FareId);
            Assert.AreEqual("5.25", feed.FareAttributes[idx].Price);
            Assert.AreEqual("USD", feed.FareAttributes[idx].CurrencyType);
            Assert.AreEqual(PaymentMethodType.OnBoard, feed.FareAttributes[idx].PaymentMethod);
            Assert.AreEqual(0, feed.FareAttributes[idx].Transfers);
            Assert.AreEqual(string.Empty, feed.FareAttributes[idx].TransferDuration);
        }
    }
}