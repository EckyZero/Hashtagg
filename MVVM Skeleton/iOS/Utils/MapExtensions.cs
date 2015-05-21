using System;
using MapKit;
using System.Collections.Generic;
using CoreLocation;
using System.Linq;
using Shared.VM;

namespace iOS
{
	public static class MapExtensions
	{
		public static void CenterAroundAnnotations(this MKMapView map, IEnumerable<IMKAnnotation> annotations = null)
		{
			if(annotations == null){
				annotations = map.Annotations;
			}
				
			double minLat = 90.0;
			double maxLat = -90.0;
			double minLon = 180.0;
			double maxLon = -180.0;

			double markerMaxWidth = 0;
			double markerMaxHeight = 0;

			foreach (var annotation in annotations) {

				var recAnnotation = (RecommendationAnnotation)annotation;

				if (recAnnotation.Coordinate.Latitude < minLat) {
					minLat = annotation.Coordinate.Latitude;
				}		
				if (recAnnotation.Coordinate.Longitude < minLon) {
					minLon = annotation.Coordinate.Longitude;
				}		
				if (recAnnotation.Coordinate.Latitude > maxLat) {
					maxLat = annotation.Coordinate.Latitude;
				}		
				if (recAnnotation.Coordinate.Longitude > maxLon) {
					maxLon = annotation.Coordinate.Longitude;
				}


				markerMaxHeight = Math.Max ((double)recAnnotation.Image.Size.Height, (double)markerMaxHeight);
				markerMaxWidth = Math.Max ((double)recAnnotation.Image.Size.Width, (double)markerMaxWidth);
			}


			double percentageWidthMarkerOccupiesOnMap = markerMaxWidth / (double)map.Frame.Width;
			double percentageHeightMarkerOccupiesOnMap = markerMaxHeight / (double)map.Frame.Height;


			//to be used if the preferred location was not set and used to delta off when the preferred location is set
			var markerCoordinateOccupancyLong = percentageWidthMarkerOccupiesOnMap * (maxLat - minLat);
			var markerCoordinateOccupancyLat = percentageHeightMarkerOccupiesOnMap * (maxLon - minLon);

			MKCoordinateSpan span = new MKCoordinateSpan(maxLat - minLat + 2 * markerCoordinateOccupancyLat, maxLon - minLon + markerCoordinateOccupancyLong);

			CLLocationCoordinate2D center = new CLLocationCoordinate2D((maxLat - (span.LatitudeDelta / 2) + markerCoordinateOccupancyLat), (maxLon - (span.LongitudeDelta / 2) + markerCoordinateOccupancyLong/2));

			var prefAnnotation = annotations.FirstOrDefault (a => { 
				return ((RecommendationAnnotation)a).MarkerType == MapMarkerType.PreferredLocation;
			});
		
//			prefAnnotation = null;

			//if preferred location is set, use it as a center and align screen around it while still showing all markers
			if(prefAnnotation != null){
				var deltaLat = prefAnnotation.Coordinate.Latitude - center.Latitude;
				var deltaLong = prefAnnotation.Coordinate.Longitude - center.Longitude;

				center = new CLLocationCoordinate2D (prefAnnotation.Coordinate.Latitude, prefAnnotation.Coordinate.Longitude);

				//the hardcoded coordinate below correspond to the width and height of the markers in use. We have to dispalce 
				//the map to accommodate for the size of the markers...
				span = new MKCoordinateSpan(maxLat - minLat + Math.Abs(deltaLat) + 5 * markerCoordinateOccupancyLat, maxLon - minLon + Math.Abs(deltaLong) +  5 * markerCoordinateOccupancyLong);
			}

			var mapRegion = new MKCoordinateRegion (center, span);

			map.CenterCoordinate = mapRegion.Center;
			map.Region = mapRegion;
		}
	}
}

