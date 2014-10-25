// Initialize an OpenTok Session object
var session = TB.initSession(sessionId);
var uuid = guid();

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
		$.post("http://localhost:12116/client/gps", { id: uuid, lat: lat, lon: lon });
	}
	);
});

// Connect to the Session using the 'apiKey' of the application and a 'token' for permission
session.connect(apiKey, token);