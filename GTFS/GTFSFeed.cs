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
using System.Collections.Generic;

namespace GTFS
{
    /// <summary>
    /// Represents an entire GTFS feed.
    /// </summary>
    public class GTFSFeed : IGTFSFeed
    {
        /// <summary>
        /// Creates a new feed.
        /// </summary>
        public GTFSFeed()
        {
            this.Agencies = new List<Agency>();
            this.CalendarDates = new List<CalendarDate>();
            this.Calendars = new List<Calendar>();
            this.FareAttributes = new List<FareAttribute>();
            this.FareRules = new List<FareRule>();
            this.FeedInfo = new List<FeedInfo>();
            this.Frequencies = new List<Frequency>();
            this.Routes = new List<Route>();
            this.Shapes = new List<Shape>();
            this.Stops = new List<Stop>();
            this.StopTimes = new List<StopTime>();
            this.Transfers = new List<Transfer>();
            this.Trips = new List<Trip>();
        }

        /// <summary>
        /// Returns the agency with the given id.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public Agency GetAgency(string agencyId)
        {
            return this.Agencies.Find(x => x.Id.Equals(agencyId));
        }

        /// <summary>
        /// Gets the list of agencies.
        /// </summary>
        public List<Agency> Agencies { get; private set; }

        /// <summary>
        /// Gets the list of stops.
        /// </summary>
        public List<Stop> Stops { get; private set; }

        /// <summary>
        /// Returns the stop with the given id.
        /// </summary>
        /// <param name="stopId"></param>
        /// <returns></returns>
        public Stop GetStop(string stopId)
        {
            return this.Stops.Find(x => x.Id.Equals(stopId));
        }

        /// <summary>
        /// Gets the list of routes.
        /// </summary>
        public List<Route> Routes { get; private set; }

        /// <summary>
        /// Gets the route with the given id.
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        public Route GetRoute(string routeId)
        {
            return this.Routes.Find(x => x.Id.Equals(routeId));
        }

        /// <summary>
        /// Gets the list of trips.
        /// </summary>
        public List<Trip> Trips { get; private set; }

        /// <summary>
        /// Gets the trip with the given id.
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        public Trip GetTrip(string tripId)
        {
            return this.Trips.Find(x => x.Id.Equals(tripId));
        }

        /// <summary>
        /// Gets the list of stop times.
        /// </summary>
        public List<StopTime> StopTimes { get; private set; }

        /// <summary>
        /// Gets the list of calendars.
        /// </summary>
        public List<Calendar> Calendars { get; private set; }

        /// <summary>
        /// Returns the calendar for the given service id.
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Calendar GetCalendar(string serviceId)
        {
            return this.Calendars.Find(x => x.ServiceId == serviceId);
        }

        /// <summary>
        /// Gets the list of calendar dates.
        /// </summary>
        public List<CalendarDate> CalendarDates { get; private set; }

        /// <summary>
        /// Returns the calendar dates for the given service id.
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public IEnumerable<CalendarDate> GetCalendarDates(string serviceId)
        {
            return this.CalendarDates.FindAll(x => x.ServiceId == serviceId);
        }

        /// <summary>
        /// Gets the list of fare attributes.
        /// </summary>
        public List<FareAttribute> FareAttributes { get; private set; }

        /// <summary>
        /// Gets the list of fare rules.
        /// </summary>
        public List<FareRule> FareRules { get; private set; }

        /// <summary>
        /// Gets the list of shapes.
        /// </summary>
        public List<Shape> Shapes { get; private set; }

        /// <summary>
        /// Returns a list of shapes for the given id.
        /// </summary>
        /// <param name="shapeId"></param>
        /// <returns></returns>
        public List<Shape> GetShapes(string shapeId)
        {
            return this.Shapes.FindAll(x => x.Id.Equals(shapeId));
        }

        /// <summary>
        /// Gets the list of frequencies.
        /// </summary>
        public List<Frequency> Frequencies { get; private set; }

        /// <summary>
        /// Gets the list of transfers.
        /// </summary>
        public List<Transfer> Transfers { get; private set; }

        /// <summary>
        /// Gets the feed info.
        /// </summary>
        public List<FeedInfo> FeedInfo { get; private set; }

        /// <summary>
        /// Adds a new agency.
        /// </summary>
        /// <param name="agency"></param>
        public void AddAgency(Agency agency)
        {
            this.Agencies.Add(agency);
        }

        /// <summary>
        /// Adds a new calendar.
        /// </summary>
        /// <param name="calendar"></param>
        public void AddCalendar(Calendar calendar)
        {
            this.Calendars.Add(calendar);
        }

        /// <summary>
        /// Adds a new calendar date.
        /// </summary>
        /// <param name="calendar"></param>
        public void AddCalendarDate(CalendarDate calendar)
        {
            this.CalendarDates.Add(calendar);
        }

        /// <summary>
        /// Adds a new fare attribute.
        /// </summary>
        /// <param name="fareAttribute"></param>
        public void AddFareAttribute(FareAttribute fareAttribute)
        {
            this.FareAttributes.Add(fareAttribute);
        }

        /// <summary>
        /// Adds a new fare rule.
        /// </summary>
        /// <param name="fareRule"></param>
        public void AddFareRule(FareRule fareRule)
        {
            this.FareRules.Add(fareRule);
        }

        /// <summary>
        /// Adds a new feed info.
        /// </summary>
        /// <param name="feedInfo"></param>
        public void AddFeedInfo(FeedInfo feedInfo)
        {
            this.FeedInfo.Add(feedInfo);
        }

        /// <summary>
        /// Adds a new frequency.
        /// </summary>
        /// <param name="frequency"></param>
        public void AddFrequency(Frequency frequency)
        {
            this.Frequencies.Add(frequency);
        }

        /// <summary>
        /// Adds a new route.
        /// </summary>
        /// <param name="route"></param>
        public void AddRoute(Route route)
        {
            this.Routes.Add(route);
        }

        /// <summary>
        /// Adds a new shape.
        /// </summary>
        /// <param name="shape"></param>
        public void AddShape(Shape shape)
        {
            this.Shapes.Add(shape);
        }

        /// <summary>
        /// Adds a new stop.
        /// </summary>
        /// <param name="stop"></param>
        public void AddStop(Stop stop)
        {
            this.Stops.Add(stop);
        }

        /// <summary>
        /// Adds a new stop time.
        /// </summary>
        /// <param name="stopTime"></param>
        public void AddStopTime(StopTime stopTime)
        {
            this.StopTimes.Add(stopTime);
        }

        /// <summary>
        /// Adds a new transfer.
        /// </summary>
        /// <param name="transfer"></param>
        public void AddTransfer(Transfer transfer)
        {
            this.Transfers.Add(transfer);
        }

        /// <summary>
        /// Adds a new trip.
        /// </summary>
        /// <param name="trip"></param>
        public void AddTrip(Trip trip)
        {
            this.Trips.Add(trip);
        }
    }
}