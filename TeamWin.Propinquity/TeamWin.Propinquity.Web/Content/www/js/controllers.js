angular.module('starter.controllers', [])

.controller('ChatCtrl', function($scope, $ionicScrollDelegate) {
    var apiKey = '45043662';
    var sessionId = '1_MX40NTA0MzY2Mn5-MTQxNDI0MDQ5MTkyNX5YRS9OcXloN0NGWW8wNmpyOVlQWG0yWEJ-UH4';
    var token = 'T1==cGFydG5lcl9pZD00NTA0MzY2MiZzaWc9MzcwYzA0Mzg3NjQyZjBhODZmNTI5YzVkZjk0ODgxYWE3NzE4ZDQyZTpzZXNzaW9uX2lkPTFfTVg0ME5UQTBNelkyTW41LU1UUXhOREkwTURRNU1Ua3lOWDVZUlM5T2NYbG9OME5HV1c4d05tcHlPVmxRV0cweVdFSi1VSDQmY3JlYXRlX3RpbWU9MTQxNDI1MTk5MSZub25jZT04OTg3ODMmcm9sZT1QVUJMSVNIRVI=';

	// Initialize an OpenTok Session object
	var session = TB.initSession(sessionId);
	var uuid = getUuid();

	$scope.messages = []

	var publisher = TB.initPublisher(apiKey, 'publisher');

	// Attach event handlers
	session.on({

		sessionConnected: function (event) {
	        // Publish the publisher we initialzed earlier (this will trigger 'streamCreated' on other
	        // clients)
	        session.publish(publisher);
	    },
	    
	    // This function runs when another client publishes a stream (eg. session.publish())
	    streamCreated: function (event) {
	        // Create a container for a new Subscriber, assign it an id using the streamId, put it inside
	        // the element with id="subscribers"
	        var subContainer = document.createElement('div');
	        subContainer.id = 'stream-' + event.stream.streamId;
	        document.getElementById('subscribers').appendChild(subContainer);

	        // Subscribe to the stream that caused this event, put it inside the container we just made
	        session.subscribe(event.stream, subContainer);
	    },

	    signal: function(event) {
	    	var parts = event.data.split('|')
	    	$scope.messages.push({ name:parts[0] + ' (' + moment(parts[2]).format('h:mm:ssa')+ ')', message:parts[1]})

		    $scope.newMessage = {
		        text: ""
		    }
			$ionicScrollDelegate.scrollBottom();
	    }
	});

    $scope.newMessage = {
        text: ""
    }

    $scope.sendMessage = function() {
    	session.signal({
    		data: 'vince|' + $scope.newMessage.text + '|' + new Date()
    	}, function(error) {
		    if (error) {
		      console.log("signal error ("
		                   + error.code
		                   + "): " + error.reason);
		    } else {
		      console.log("signal sent.");
		    }
    	})
    }

	$(function() {
		var response = geoFindMe(function(lat, lon) {
		    $.post("http://propinquity-server.azurewebsites.net/client/gps", { id: uuid, lat: lat, lon: lon });
		});
	});

	// Connect to the Session using the 'apiKey' of the application and a 'token' for permission
	session.connect(apiKey, token);
})

.controller('VideoCtrl', function($scope) {
    var apiKey = '45043662';
    var sessionId = '1_MX40NTA0MzY2Mn5-MTQxNDI0MDQ5MTkyNX5YRS9OcXloN0NGWW8wNmpyOVlQWG0yWEJ-UH4';
    var token = 'T1==cGFydG5lcl9pZD00NTA0MzY2MiZzaWc9MzcwYzA0Mzg3NjQyZjBhODZmNTI5YzVkZjk0ODgxYWE3NzE4ZDQyZTpzZXNzaW9uX2lkPTFfTVg0ME5UQTBNelkyTW41LU1UUXhOREkwTURRNU1Ua3lOWDVZUlM5T2NYbG9OME5HV1c4d05tcHlPVmxRV0cweVdFSi1VSDQmY3JlYXRlX3RpbWU9MTQxNDI1MTk5MSZub25jZT04OTg3ODMmcm9sZT1QVUJMSVNIRVI=';

	// Initialize an OpenTok Session object
	var session = TB.initSession(sessionId);
	var uuid = getUuid();

	// Initialize a Publisher, and place it into the element with id="publisher"
	var publisher = TB.initPublisher(apiKey, 'publisher');

	// Attach event handlers
	session.on({

	    // This function runs when session.connect() asynchronously completes
	    sessionConnected: function (event) {
	        // Publish the publisher we initialzed earlier (this will trigger 'streamCreated' on other
	        // clients)
	        session.publish(publisher);
	    },

	    // This function runs when another client publishes a stream (eg. session.publish())
	    streamCreated: function (event) {
	        // Create a container for a new Subscriber, assign it an id using the streamId, put it inside
	        // the element with id="subscribers"
	        var subContainer = document.createElement('div');
	        subContainer.id = 'stream-' + event.stream.streamId;
	        document.getElementById('subscribers').appendChild(subContainer);

	        // Subscribe to the stream that caused this event, put it inside the container we just made
	        session.subscribe(event.stream, subContainer);
	    }
	});

	$(function() {
		geoFindMe(function(lat, lon) {
		    $.post("http://propinquity-server.azurewebsites.net/client/gps", { id: uuid, lat: lat, lon: lon });
		});
	});

	// Connect to the Session using the 'apiKey' of the application and a 'token' for permission
	session.connect(apiKey, token);
})

.controller('FriendsCtrl', function($scope, Friends) {
  $scope.friends = Friends.all();
})

.controller('FriendDetailCtrl', function($scope, $stateParams, Friends) {
  $scope.friend = Friends.get($stateParams.friendId);
})

.controller('AccountCtrl', function($scope) {
});
