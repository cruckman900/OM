"use strict";

var thegroup = null; //the missionID
var theteam = null;

var currentGMT = 0;
var currentADV = 0;
var currentMCGMT = 0;

function initGroup(group) {
    thegroup = group;
}

function initTeam(team) {
    if (team != null)
        theteam = team;
    else
        theteam = "FlightDirector";
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveMessage", function (user, sendFrom, sendTo, message) {
    var msg = message.replace(/&/g, "&amp;")
        .replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var d = new Date();
    var n = d.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    var fromto = n + " - From " + sendFrom + " To " + sendTo + ": ";
    var encodedMsg = msg;

    if (sendFrom == theteam || sendTo == theteam || sendTo == "All" || theteam == null || window.location.href.indexOf("FlightDirector") > 0) {
        var li = document.createElement("li");
        li.innerHTML = fromto.bold() + encodedMsg;
        li.className = "list-group-flush";

        document.getElementById("messagesList").insertBefore(li, document.getElementById("messagesList").childNodes[0]);
        document.getElementById("messageInput").value = "";
        document.getElementById("messageInput").focus();
    }
});

connection.on("ReceiveDataPush", function (team, currentRecord, timerInterval) {
    if (currentRecord == null || timerInterval == null)
        return false;

    var table = null;

    if (window.location.href.indexOf("Volcano") > 0 || window.location.href.indexOf("Hurricane") > 0
        || window.location.href.indexOf("MedComm") > 0) {

        document.getElementById("mainDiv").classList.remove("d-none");
        document.getElementById("standingBy").classList.add("d-none");

        if (team == "Volcano") {
            table = document.getElementById("tblVolcDataDrive");
        }
        else if (team == "Hurricane") {
            table = document.getElementById("tblHurcDataDrive");
        }
        else if (team == "MedComm") {
            table = document.getElementById("tblMedComDataDrive");
        }

        if (table != null)  {
            for (var i = table.rows.length - 1; i >= table.rows.length - currentRecord; i--) {
                table.rows[i].classList.remove("d-none");
                table.rows[i].classList.remove("bg-primary");
                if (i == table.rows.length - currentRecord)
                    table.rows[i].classList.add("bg-primary");
            }
        }
    }
});

connection.on("ReceiveDataDriveData", function (data) {
    // display the data drive data in the tables on the interfaces
    var table = null;
    var temp = null;

    if (document.getElementById("tblVolcData")) {
        temp = document.getElementById("tblVolcData");

        // delete all rows in the tbody for volcano team data
        for (var i = temp.rows.length - 1; i > 0; i--) {
            temp.deleteRow(i);
        }
    }
    if (document.getElementById("tblHurcData")) {
        // delete all rows in the tbody for hurricane team data
        temp = document.getElementById("tblHurcData");

        for (var i = temp.rows.length - 1; i > 0; i--) {
            temp.deleteRow(i);
        }
    }
    if (document.getElementById("tblEvacData")) {
        // delete all rows in the tbody for the evacuation team data
        temp = document.getElementById("tblEvacData");

        for (var i = temp.rows.length - 1; i > 0; i--) {
            temp.deleteRow(i);
        }
    }
    if (document.getElementById("tblMedCommData")) {
        // delete all rows in the tbody for the medcomm team data
        temp = document.getElementById("tblMedCommData");

        for (var i = temp.rows.length - 1; i > 0; i--) {
            temp.deleteRow(i);
        }
    }

    // for each team, build the tbody table rows with the team's data rows
    for (let i = 0; i < data.length; i++) {
        var input = JSON.parse(data[i].dataInput);

        if (data[i].from == 5 && data[i].to == 6) { // FD
            displayTab(input.Display);
        }

        if (data[i].from == 1) { // Hurricane
            table = document.getElementById("tblHurcData");

            if (table) {
                var tr = document.createElement("tr");

                var adv = document.createElement("td");
                adv.innerHTML = input.ADV;
                tr.append(adv);

                var lat = document.createElement("td");
                lat.innerHTML = input.Lat;
                tr.append(lat);

                var lng = document.createElement("td");
                lng.innerHTML = input.Lng;
                tr.append(lng);

                var cat = document.createElement("td");
                cat.innerHTML = input.Cat;
                tr.append(cat);

                var distTrav = document.createElement("td");
                distTrav.innerHTML = input.DistTrav;
                tr.append(distTrav);

                var speed = document.createElement("td");
                speed.innerHTML = input.Speed;
                tr.append(speed);

                var direction = document.createElement("td");
                direction.innerHTML = input.Direction;
                tr.append(direction);

                var distIsl = document.createElement("td");
                distIsl.innerHTML = input.DistIsl;
                tr.append(distIsl);

                var eta = document.createElement("td");
                eta.innerHTML = input.ETA;
                tr.append(eta);

                table.prepend(tr);
            }
        }

        if (data[i].from == 2) { // Volcano
            table = document.getElementById("tblVolcData");

            if (table) {
                var tr = document.createElement("tr");

                var gmt = document.createElement("td");
                gmt.innerHTML = input.GMT;
                tr.append(gmt);

                var hourlyVT = document.createElement("td");
                hourlyVT.innerHTML = input.HourlyVT;
                tr.append(hourlyVT);

                var cumulVT = document.createElement("td");
                cumulVT.innerHTML = input.CumulVT;
                tr.append(cumulVT);

                var projVT = document.createElement("td");
                projVT.innerHTML = input.ProjVT;
                tr.append(projVT);

                var hourlyRF = document.createElement("td");
                hourlyRF.innerHTML = input.HourlyRF;
                tr.append(hourlyRF);

                var cumulRF = document.createElement("td");
                cumulRF.innerHTML = input.CumulRF;
                tr.append(cumulRF);

                var projRF = document.createElement("td");
                projRF.innerHTML = input.ProjRF;
                tr.append(projRF);

                var tSeismic = document.createElement("td");
                tSeismic.innerHTML = input.TSeismic;
                tr.append(tSeismic);

                table.prepend(tr);
            }
        }

        if (data[i].from == 3) { // Evacuation
            table = document.getElementById("tblEvacData");

            if (table) {
                var tr = document.createElement("tr");

                var loc = document.createElement("td");
                loc.innerHTML = input.Location;
                tr.append(loc);

                var pop = document.createElement("td");
                pop.innerHTML = input.Population;
                tr.append(pop);

                var trans = document.createElement("td");
                trans.innerHTML = input.Transportation;
                tr.append(trans);

                var dest = document.createElement("td");
                dest.innerHTML = input.Destination;
                tr.append(dest);

                table.prepend(tr);

                // call the canvas.js file and update the circle colors for evacuation status
                if (window.location.href.indexOf("Evacuation") > 0)
                    redrawCircles(input);
            }
        }

        if (data[i].from == 4) { // MedComm
            table = document.getElementById("tblMedCommData");

            if (table) {
                var tr = document.createElement("tr");
                tr.className = "trLink";

                var distributiontype = document.createElement("td");
                distributiontype.innerHTML = input.DistributionType;
                tr.append(distributiontype);

                var headline = document.createElement("td");
                headline.innerHTML = input.Headline;
                tr.append(headline);

                var bulletin = document.createElement("td");
                bulletin.innerHTML = input.Bulletin;
                bulletin.className = "d-none";
                tr.append(bulletin);

                tr.addEventListener("click", function () {
                    showBulletin(data[i].dataInput);
                });

                table.prepend(tr);
            }
        }
    }
});

connection.on("ReceiveConnectionState", function (user, connected) {
    // Used to display online status of teams on the flight director interface
    if (window.location.href.indexOf("FlightDirector") > 0)
        showOnlineOffline(user, connected);
});

connection.on("ReceiveCurrentGMT", function (gmt) {
    // Call to the chathub to receive the current time from the volcano team
    try {
        if (document.getElementById("currentGMT") != null) {
            currentGMT = gmt;
            document.getElementById("currentGMT").value = gmt;
        }
    } catch (err) {
        console.log(err.message);
    }
})

connection.on("ReceiveCurrentADV", function (adv) {
    // call to the chathub to receive the current advisory form the hurricane team
    try {
        if (document.getElementById("currentADV") != null) {
            currentADV = adv;
            document.getElementById("currentADV").value = adv;
        }
    } catch (err) {
        console.log(err.message);
    }
})

connection.on("ReceiveCurrentMCGMT", function (gmt) {
    //call to the chathub to receive the current time for the medcomm team
    try {
        if (document.getElementById("currentMCGMT") != null) {
            currentMCGMT = gmt;
            document.getElementById("currentMCGMT").value = gmt;
        }
    } catch (err) {
        console.log(err.message);
    }
})

document.getElementById("sendButton")
    .addEventListener("click", function (event) {
        // Handle the onclick event of the send button in the chat interface and send message
        var user = document.getElementById("userInput").value;
        var group = thegroup;
        var sendTo = document.getElementById("sendToInput").value;
        var sendFrom = document.getElementById("sendFromInput").value;
        var message = document.getElementById("messageInput").value;

        if (message != null) {
            // call to chathub to send message
            connection.invoke("SendMessageToGroup", user, group, sendFrom, sendTo, message)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        event.preventDefault();
    });

function SendDataDriveData(missionID, data) {
    connection.invoke("SendDataDriveData", missionID, data).catch(err => console.error(err.toString()));
}

function GetDataDriveData(thegroup) {
    connection.invoke("GetDataDriveData", thegroup).catch(err => console.error(err.toString()));
}

function updateData(MissionID, obj) {
    connection.invoke("SendDataPushToGroup", MissionID, obj.Team, obj.CurrentRecord, obj.CurrentInterval)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

// used to display coordinates for mouse click on evac team map during setting up points
// uncomment alert to get position
function updateCurrentADVPos(teamName, Pos) {
    //alert("updateCurrentADVPos " + teamName + '": ' + Pos);
}

function displayCurrentGMT(missionID, gmt) {
    connection.invoke("GetCurrentGMT", parseInt(missionID, 10), gmt);
}

function displayCurrentADV(missionID, adv) {
    connection.invoke("GetCurrentADV", parseInt(missionID, 10), adv);
}

function displayCurrentMCGMT(missionID, gmt) {
    connection.invoke("GetCurrentMCGMT", parseInt(missionID, 10), gmt);
}

async function start() {
    try {
        // on start, get state of objects and teams
        Object.defineProperty(WebSocket, 'OPEN', { value: 1, });
        await connection.start().catch(function (err) {
            return console.error(err.toString());
        });
        console.log('connected');

        // add user to group (the mission)
        connection.invoke("AddToGroup", thegroup).catch(err => console.error(err));
        connection.invoke("Connect", theteam, thegroup).catch(err => console.error(err.toString()));

        // get the connection status of the teams
        connection.invoke("ReceiveConnectionState", thegroup, theteam, true).catch(err => console.error(err.toString()));

        if (theteam == "Volcano" || theteam == "Hurricane" || theteam == "MedComm")
            connection.invoke("SetDataPushOnLoad", thegroup, theteam).catch(err => console.error(err.toString()));

        // get the current records for the volcano, hurricane and medcomm teams
        if (theteam == "Volcano" || theteam == "Hurricane" || theteam == "MedComm")
            connection.invoke("GetFDDataPushRecord", thegroup, theteam).catch(err => console.error(err.toString()));

        // get the data for the teams and display in the appropriate interfaces
        connection.invoke("GetDataDriveData", thegroup).catch(err => console.error(err.toString()));

        // get the current logged in status of the teams
        if (window.location.href.indexOf("FlightDirector") > 0) {
            var connectedUsers = connection.invoke("GetLoggedInUsers", thegroup).catch(err => console.error(err.toString()));
            console.log(connectedUsers);
        }

        // get the current time from the volcano team
        try {
            getCurrentGMT(thegroup);
        } catch { }

        // get the current advisory from the hurricane team
        try {
            getCurrentADV(thegroup);
        } catch { }

        // get the current time from the medcomm team
        try {
            getCurrentMCGMT(thegroup);
        } catch { }

    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    // perform functions when a team goes offline and attempt to reconnect to the hub
    console.log("disconnected");
    disconnectNotice();
    await start();
});

start();