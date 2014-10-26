var sessionId;
var session;
var publisher;

function onSessionIdChanged(sessionId, token) {
    if (session) {
        console.log('onSessionIdChanged, disconnecting the previous session...');
        session.off();
        session.on({
            sessionDisconnected: function (event) {
                console.log("The session disconnected: " + event.reason);
                console.log("initialising a new session: " + sessionId);
                
            	session.off();
            	publisher.off();
                
            	publisher.on({
            		destroyed: function () {
            		    console.log("publisher motherfucking destroyed");
            		    publisher.off();
            			initializeSession(sessionId, token);
            		}
            	});
            	publisher.destroy();
            }
        });
        session.disconnect();
    } else {
        console.log('onSessionIdChanged, no previous session, initializing straight away...');
        initializeSession(sessionId, token);
    }
}

function initializeSession(sessionId, token) {
    // Initialize an OpenTok Session object
    session = TB.initSession(sessionId);

    // Initialize a Publisher, and place it into the element with id="publisher"
    publisher = TB.initPublisher(apiKey, 'publisher');

    // Attach event handlers
    session.on({
        // This function runs when session.connect() asynchronously completes
        sessionConnected: function (event) {

		    sessionId = event.target.sessionId;
		    console.log("sessionConnected: " + event.target.sessionId);
		    // Publish the publisher we initialzed earlier (this will trigger 'streamCreated' on other
		    // clients)
		    session.publish(publisher);
        },

        sessionDisconnected: function (event) {
            console.log("sessionDisconnected: " + event.reason);
        },

        // This function runs when another client publishes a stream (eg. session.publish())
        streamCreated: function (event) {
            console.log('streamCreated: event.stream.streamId: ' + event.stream.streamId);

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
            $('#messages').prepend('<li><span>' + parts[0] + ' (' + moment(parts[2]).format('h:mm:ssa')+ ')</span><p>' + parts[1]  + '</p></li>');
        },

        streamDestroyed: function (event) {
            console.log("streamDestroyed: the name is " + event.stream.name + ", the reason is " + event.reason + '.');
        },

        connectionDestroyed: function (event) {
            console.log('connectionDestroyed: connection Id: ' + event.connection.connectionId);
        }
    });

    // Connect to the Session using the 'apiKey' of the application and a 'token' for permission
    session.connect(apiKey, token);

    $('button#send').on('click', function() {
        session.signal({
            data: 'vince|' + $('input[name=message]').val() + '|' + new Date()
        }, function(error) {
            if (error) {
                console.log("signal error ("
                             + error.code
                             + "): " + error.reason);
            } else {
                console.log("signal sent.");
            }
        })
        $('input[name=message]').val('')
    });

    var interval = setInterval(function() {
        $('.OT_video-container').each(function() {
            if ($(document).width() < 992) {
                $(this).parent().width('100%');
            } else {
                $(this).parent().width('50%').css('float', 'left');
            };
        });
    });
}

$(function () {
    var uuid = getUuid();
    console.log('Document ready, uuid is: ' + uuid + '.');
    geoFindMe(function (lat, lon) {
        console.log('Got GPS, lat: ' + lat + ', lon: ' + lon + '.');
        
        $.post(serverUrl + "client/gps", { id: uuid, lat: lat, lon: lon })
         .done(function (data) {
             if (sessionId != data.SessionId) {
                 console.log('Posted GPS and session changed from: ' + sessionId + ', to: ' + data.SessionId + '.');
                 sessionId = data.SessionId;
		         onSessionIdChanged(data.SessionId, data.Token);
             } else {
	             console.log("The session didn't motherfucking change");
             }
         });
    });
});



