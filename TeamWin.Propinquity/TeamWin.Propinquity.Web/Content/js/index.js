var sessionId;
var session;
var publisher;
var myAvatarName;

function onSessionIdChanged(sessionId, token, avatarName) {
	myAvatarName = avatarName;
	log('AvatarName is: ' + myAvatarName);
	if (session) {
        log('onSessionIdChanged, disconnecting the previous session...');
        session.off();
        session.on({
            sessionDisconnected: function (event) {
                log("The session disconnected: " + event.reason);
                log("initialising a new session: " + sessionId);
                
            	session.off();
            	publisher.off();
                
            	publisher.on({
            		destroyed: function () {
            		    log("publisher motherfucking destroyed");
            		    publisher.off();
            			initializeSession(sessionId, token);
            		}
            	});
            	publisher.destroy();
            }
        });
        session.disconnect();
    } else {
        log('onSessionIdChanged, no previous session, initializing straight away...');
        initializeSession(sessionId, token);
    }
}

function initializeSession(sessionId, token) {
    log('initializing session');

    // Initialize an OpenTok Session object
    session = TB.initSession(sessionId);

    // Initialize a Publisher, and place it into the element with id="publisher"
    publisher = TB.initPublisher(apiKey, 'publisher');

    // Attach event handlers
    session.on({
        // This function runs when session.connect() asynchronously completes
        sessionConnected: function (event) {

		    sessionId = event.target.sessionId;
		    log("sessionConnected: " + event.target.sessionId);
		    // Publish the publisher we initialzed earlier (this will trigger 'streamCreated' on other
		    // clients)
		    session.publish(publisher);
        },

        sessionDisconnected: function (event) {
            log("sessionDisconnected: " + event.reason);
        },

        // This function runs when another client publishes a stream (eg. session.publish())
        streamCreated: function (event) {
            log('streamCreated: event.stream.streamId: ' + event.stream.streamId);

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
            log("streamDestroyed: the name is " + event.stream.name + ", the reason is " + event.reason + '.');
        },

        connectionDestroyed: function (event) {
            log('connectionDestroyed: connection Id: ' + event.connection.connectionId);
        }
    });

    log('connecting');

    // Connect to the Session using the 'apiKey' of the application and a 'token' for permission
    session.connect(apiKey, token);

    $('button#send').on('click', function() {
        session.signal({
        	data: myAvatarName + '|' + $('input[name=message]').val() + '|' + new Date()
        }, function(error) {
            if (error) {
                log("signal error ("
                             + error.code
                             + "): " + error.reason);
            } else {
                log("signal sent.");
            }
        })
        $('input[name=message]').val('')
    });

    var interval = setInterval(function () {
        var totalInRoom = $('.OT_video-container').size();
        var videoIndex = 0;
        $('.OT_video-container').each(function () {
            if ((totalInRoom == 1) ||
                ($(document).width() < 992) ||
                ((totalInRoom % 2 == 1) && (videoIndex == totalInRoom - 1))) {
                $(this).parent().width('100%');
            } else {
                $(this).parent().width('50%').css('float', 'left');
            };
            videoIndex++;
        });
    });
}

$(function () {
    var uuid = getUuid();
    log('Document ready, uuid is: ' + uuid + '.');
    geoFindMe(function (lat, lon) {
        log('Got GPS, lat: ' + lat + ', lon: ' + lon + '.');
        
        $.post(serverUrl + "client/gps", { id: uuid, lat: lat, lon: lon })
         .done(function (data) {
             if (sessionId != data.SessionId) {
                 log('Posted GPS and session changed from: ' + sessionId + ', to: ' + data.SessionId + '.');
                 sessionId = data.SessionId;
		         onSessionIdChanged(data.SessionId, data.Token, data.AvatarName);
             } else {
	             log("The session didn't motherfucking change");
             }
         });
    });
});



