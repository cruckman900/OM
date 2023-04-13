// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var password;
var MissionID = null;

var volcObject = {
    Team: "Volcano",
    Abbr: "Volc",
    TeamID: null,
    DataInterval: 0,
    CurrentRecord: 1,
    Timer: null,
    CurrentInterval: 0,
    DataIntervalElem: "#volcInterval",
    NextReadingElem: "#volcNextReading",
    SendDataElem: "#sendVolcData"
}

var hurcObject = {
    Team: "Hurricane",
    Abbr: "Hurc",
    TeamID: null,
    DataInterval: 0,
    CurrentRecord: 1,
    Timer: null,
    CurrentInterval: 0,
    DataIntervalElem: "#hurcInterval",
    NextReadingElem: "#hurcNextReading",
    SendDataElem: "#sendHurcData"
}

var medcObject = {
    Team: "MedComm",
    Abbr: "MedC",
    TeamID: null,
    DataInterval: 0,
    CurrentRecord: 1,
    Timer: null,
    CurrentInterval: 0,
    DataIntervalElem: "#medcommInterval",
    NextReadingElem: "#medcommNextReading",
    SendDataElem: "#sendMedcommData"
}

function getTeamID(missionID, teamName) {
    MissionID = missionID;
    var teamNameID = getUserEnumInt(teamName);
    $.ajax({
        type: "GET",
        async: false,
        url: "/ChatAndData/GetTeamID",
        data: { pMissionID: MissionID, pTeamID: teamNameID },
        dataType: "json",
        cache: false,
        success: function (result) {
            if (teamName == "Hurricane") {
                hurcObject.TeamID = result;
                initTeam(hurcObject);
            }
            if (teamName == "Volcano") {
                volcObject.TeamID = result;
                initTeam(volcObject);
            }
            if (teamName == "MedComm") {
                medcObject.TeamID = result;
                initTeam(medcObject);
            }

            return true;
        },
        error: function () {
            alert("Make sure that the " + teamName + " team has logged in.");
        }
    });
}

function initTeam(obj) {
    clearInterval(obj.Timer);
    clearInterval(obj.CurrentInterval);
    $(obj.NextReadingElem).val(null);
    $(obj.SendDataElem).val("Send " + obj.Abbr + " Data");
}

// on change, set the timer intervals and change button text
$("#volcInterval").change(function () {
    volcObject.DataInterval = $("#volcInterval").val() * 60000;
    clearInterval(volcObject.Timer);   // timer for sending the data drive to team
    clearInterval(volcObject.CurrentInterval);  // countdown timer for flight director and communication screens
    $("#volcNextReading").val(null);
    $("#sendVolcData").val("Send Volc Data");
});

$("#hurcInterval").change(function () {
    hurcObject.DataInterval = $("#hurcInterval").val() * 60000;
    clearInterval(hurcObject.Timer);   // timer for sending the data drive to team
    clearInterval(hurcObject.CurrentInterval);  // countdown timer for flight director and communication screens
    $("#hurcNextReading").val(null);
    $("#sendHurcData").val("Send Hurc Data");
});

$("#medcommInterval").change(function () {
    medcObject.DataInterval = $("#medcommInterval").val() * 60000;
    clearInterval(medcObject.Timer);// timer for sending the data drive to team
    clearInterval(medcObject.CurrentInterval)// countdown timer for flight director and communication screens
    $("#medcommNextReading").val(null);
    $("#sendMCData").val("Send MedC Data");
})

// set up the onclick events for the buttons: start/stop timer, change button text and functionality
$("#sendVolcData").click(function () {
    clearInterval(volcObject.CurrentInterval);
    $("#volcNextReading").val(null);
    if (volcObject.TeamID == null)
        getTeamID(MissionID, "Volcano");
    if ($("#sendVolcData").val() == "Send Volc Data") {
        getDataDrive(MissionID, "Volcano", volcObject.TeamID, volcObject.DataInterval);
        volcObject.Timer = setInterval(alertVolcTimer, volcObject.DataInterval);

        $("#sendVolcData").val("Pause Volc Data");
    } else {
        clearInterval(volcObject.Timer);
        $("#sendVolcData").val("Send Volc Data");
    }
});

$("#sendHurcData").click(function () {
    clearInterval(hurcObject.CurrentInterval);
    $("#hurcNextReading").val(null);
    if (hurcObject.TeamID == null)
        getTeamID(MissionID, "Hurricane");
    if ($("#sendHurcData").val() == "Send Hurc Data") {
        getDataDrive(MissionID, "Hurricane", hurcObject.TeamID, hurcObject.DataInterval);
        hurcObject.Timer = setInterval(alertHurcTimer, hurcObject.DataInterval);

        $("#sendHurcData").val("Pause Hurc Data");
    } else {
        clearInterval(hurcObject.Timer);
        $("#sendHurcData").val("Send Hurc Data");
    }
})

$("#sendMCData").click(function () {
    clearInterval(medcObject.CurrentInterval);
    $("#medcommNextReading").val(null);
    if (medcObject.TeamID == null)
        getTeamID(MissionID, "MedComm");
    if ($("#sendMCData").val() == "Send MedC Data") {
        getDataDrive(MissionID, "MedComm", medcObject.TeamID, medcObject.DataInterval);
        medcObject.Timer = setInterval(alertMedCommTimer, medcObject.DataInterval);
        $("#sendMCData").val("Pause MedC Data");
    } else {
        clearInterval(medcObject.Timer);
        $("#sendMCData").val("Send MedC Data");
    }
})

function CountdownTimerForVolc() {
    CountDownTimer(volcObject);
}

function CountdownTimerForHurc() {
    CountDownTimer(hurcObject);
}

function CountdownTimerForMedComm() {
    CountDownTimer(medcObject);
}

function CountDownTimer(obj) {
    var time = obj.DataInterval / 60000 + ":00";
    clearInterval(obj.CurrentInterval);
    obj.CurrentInterval = setInterval(function () {
        var timer = time.split(":");
        var minutes = parseInt(timer[0], 10);
        var seconds = parseInt(timer[1], 10);
        --seconds;
        minutes = (seconds < 0) ? --minutes : minutes;
        if (minutes < 0) {
            $(obj.NextReadingElem).val("");
            clearInterval(obj.CurrentInterval);
        } else {
            seconds = (seconds < 0) ? 59 : seconds;
            seconds = (seconds < 10) ? '0' + seconds : seconds;
            $(obj.NextReadingElem).val(minutes + ":" + seconds);
            time = minutes + ":" + seconds;
        }
    }, 1000);
}

function alertHurcTimer() {
    hurcObject.CurrentRecord = hurcObject.CurrentRecord + 1;
    insertUpdateDataPush(MissionID, "Hurricane", hurcObject.TeamID, hurcObject.CurrentRecord, hurcObject.DataInterval);
    updateData(MissionID, hurcObject);
}

function alertMedCommTimer() {
    medcObject.CurrentRecord = medcObject.CurrentRecord + 1;
    insertUpdateDataPush(MissionID, "MedComm", medcObject.TeamID, medcObject.CurrentRecord, medcObject.DataInterval);
    updateData(MissionID, medcObject);
}

function alertVolcTimer() {
    volcObject.CurrentRecord = volcObject.CurrentRecord + 1;
    insertUpdateDataPush(MissionID, "Volcano", volcObject.TeamID, volcObject.CurrentRecord, volcObject.DataInterval);
    updateData(MissionID, volcObject);
}

function getCurrentGMT(missionID) {
    MissionID = missionID;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/GetCurrentGMT",
        data: { pMissionID: missionID },
        cache: false,
        success: function (result) {
            if (typeof result !== "undefined") {
                displayCurrentGMT(missionID, result.gmt);
                CountdownTimerForVolc();
            } else {
                clearInterval(volcObject.Timer);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("getCurrentGMT " + textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    });
}

function getCurrentADV(missionID) {
    MissionID = missionID;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/GetCurrentADV",
        data: { pMissionID: missionID },
        cache: false,
        success: function (result) {
            if (typeof result !== "undefined") {
                displayCurrentADV(missionID, result.adv);
                CountdownTimerForHurc();
            } else {
                clearInterval(hurcObject.Timer);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("getCurrentADV " + textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    });
}

function getCurrentMCGMT(missionID) {
    MissionID = missionID;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/GetCurrentMCGMT",
        data: { pMissionID: missionID },
        cache: false,
        success: function (result) {
            if (typeof result !== "undefined") {
                displayCurrentMCGMT(missionID, result.gmt);
                CountdownTimerForMedComm();
            } else {
                clearInterval(medcObject.Timer);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("getCurrentMCGMT " + textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    })
}

var currentFontSize = parseInt($("#FDScript > div").css("font-size"));
currentFontSize = 18;

var isInit = false;
//Script font size
function resizeText(flag) {
    if (currentFontSize != 18 && isInit == false) {
        currentFontSize = 18;
        isInit = true;
    }

    if (flag == "increase")
        currentFontSize += 2;
    else
        currentFontSize -= 2;

    $("#FDScript *").css("font-size", currentFontSize);
}

//#endregion FD

//#region Chat

$("#sendButton").click(function () {
    // gather the variables from the page and submit them to the insert function
    var missionID = document.getElementById("missionID").value;
    MissionID = missionID;
    var from = document.getElementById("sendFromInput").value;
    var to = document.getElementById("sendToInput").value;
    var message = document.getElementById("messageInput").value;

    insertChatMessage(missionID, getUserEnumInt(from), getUserEnumInt(to), message);
});

$("#btnSendVolcData").click(function () {
    var missionID = document.getElementById("missionID").value;
    MissionID = missionID;
    var from = "Volcano";
    var to = "FD";

    var gmtIndex = document.getElementById("GMT").selectedIndex;
    var gmtOptions = document.getElementById("GMT").options;

    var gmt = gmtOptions[gmtIndex].text;
    var hourlyVT = document.getElementById("HourlyVT").value;
    var cumulVT = document.getElementById("CumulVT").value;
    var projVT = document.getElementById("ProjVT").value;
    var hourlyRF = document.getElementById("HourlyRF").value;
    var cumulRF = document.getElementById("CumulRF").value;
    var projRF = document.getElementById("ProjRF").value;
    var tSeismic = document.getElementById("TSeismic").value;

    var dataInput = '{"GMT": "' + gmt + '", "HourlyVT": "' + hourlyVT + '", "CumulVT": "' + cumulVT + '", ' +
        '"ProjVT": "' + projVT + '", "HourlyRF": "' + hourlyRF + '", "CumulRF": "' + cumulRF + '", ' +
        '"ProjRF": "' + projRF + '", "TSeismic": "' + tSeismic + '"}';

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
});

$("#btnSendHurcData").click(function () {
    var missionID = document.getElementById("missionID").value;
    MissionID = missionID;
    var from = "Hurricane";
    var to = "FD";

    var advIndex = document.getElementById("ADV").selectedIndex;
    var advOptions = document.getElementById("ADV").options

    var adv = advOptions[advIndex].text;
    var lat = document.getElementById("Lat").value;
    var lng = document.getElementById("Lng").value;

    var catIndex = document.getElementById("Cat").selectedIndex;
    var catOptions = document.getElementById("Cat").options;

    var cat = catOptions[catIndex].text;
    var distTrav = document.getElementById("DistTrav").value;
    var speed = document.getElementById("Speed").value;
    var direction = document.getElementById("Direction").value;
    var distIsl = document.getElementById("DistIsl").value;
    var eta = document.getElementById("ETA").value;

    var dataInput = '{"ADV": "' + adv + '", "Lat": "' + lat + '", "Lng": "' + lng + '", "Cat": "' + cat + '", ' +
        '"DistTrav": "' + distTrav + '", "Speed": "' + speed + '", "Direction": "' + direction + '", ' +
        '"DistIsl": "' + distIsl + '", "ETA": "' + eta + '"}';

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
});

$("#btnSendEvacData").click(function () {
    var missionID = thegroup;
    MissionID = missionID;
    var from = "Evacuation";
    var to = "FD";

    var x = document.getElementById("x").value;
    var y = document.getElementById("y").value;
    var circleSize = document.getElementById("circleSize").value;

    var loc = document.getElementById("evacFrom").value;
    var pop = document.getElementById("evacPop").value;
    var trans = document.getElementById("evacTrans").value;
    var dest = document.getElementById("evacTo").value;
    var complete = document.getElementById("evacComplete").checked;

    var newColor = (complete == true) ? "#58FA58" : "#ffff00";

    var dataInput = '{"Location": "' + loc + '", "Population": "' + pop + '", "Transportation": "' + trans + '", ' +
        '"Destination": "' + dest + '", "Complete": "' + complete + '", "x": "' + x + '", "y": "' + y + '", ' +
        '"circleSize": "' + circleSize + '", "color": "' + newColor + '"}';

    var data = new Object();
    data.MissionID = missionID;
    data.From = from;
    data.To = to;
    data.DataInput = dataInput;

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
});

$("#btnSendMedCommData").click(function () {
    var missionID = thegroup;
    MissionID = missionID;
    var from = "MedComm";
    var to = "FD";

    var DistributionType = document.getElementById("distributiontype").value;
    var Headline = document.getElementById("headline").value;
    var Bulletin = document.getElementById("bulletinbody").value.replace(/\n/g, "<br />");

    var dataInput = '{"DistributionType": "' + DistributionType + '", "Headline": "' + Headline + '", ' +
        '"Bulletin": "' + Bulletin + '"}';

    var data = new Object();
    data.MissionID = missionID;
    data.From = from;
    data.To = to;
    data.DataInput = dataInput;

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
})

$("#sendArchivedData").click(function () {
    var missionID = thegroup;
    MissionID = missionID;
    var from = "FD";
    var to = "All";

    var dataInput = '{"Display": "ArchivedData"}';

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
})

$("#sendVolcVideo").click(function () {
    var missionID = thegroup;
    MissionID = missionID;
    var from = "FD";
    var to = "All";

    var dataInput = '{"Display": "VolcVideo"}';

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
})

$("#sendPostBrief").click(function () {
    var missionID = thegroup;
    MissionID = missionID;
    var from = "FD";
    var to = "All";

    var dataInput = '{"Display": "PostBrief"}';

    insertDataDriveData(missionID, from, getUserEnumInt(from), getUserEnumInt(to), dataInput);
})

function showBulletin(data) {
    var input = JSON.parse(data);

    document.getElementById("_headline").innerHTML = input.Headline;
    document.getElementById("_distributiontype").innerHTML = input.DistributionType;
    document.getElementById("_bulletin").innerHTML = input.Bulletin;
    $("#bulletinModal").modal("show");
}

function displayTab(tab) {
    $("#mainDiv").removeClass("d-none");
    $("#standingBy").addClass("d-none");

    if (tab == "ArchivedData") {
        $("#li_archive").removeClass("d-none");
    }
    if (tab == "VolcVideo") {
        $("#li_video").removeClass("d-none");
    }
    if (tab == "PostBrief") {
        $("#li_questions").removeClass("d-none");
    }
}

function getUserEnumInt(str) {
    // convert the enum string to the index integer to insert into the database table

    var intUser = 0;
    switch (str) {
        case "Communications":
            intUser = 0;
            break;
        case "Hurricane":
            intUser = 1;
            break;
        case "Volcano":
            intUser = 2;
            break;
        case "Evacuation":
            intUser = 3;
            break;
        case "MedComm":
            intUser = 4;
            break;
        case "FD":
            intUser = 5;
            break;
        case "All":
            intUser = 6;
            break;
    }
    return intUser;
}

// inserts the chat information into the database
function insertChatMessage(missionID, from, to, message) {
    // make an ajax call to post the chat message to the server
    var oChat = new Object();
    oChat.MissionID = missionID;
    MissionID = missionID;
    oChat.From = from;
    oChat.To = to;
    oChat.Message = message;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/ChatAndData/InsertChatMessage",
        data: oChat,
        succss: function (data) {
            // do nothing
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ": " + errorThrown + ", " + jqXHR.responseText);
        }
    });
}

function insertDataDriveData(missionID, team, from, to, dataInput) {
    var oData = new Object();
    oData.MissionID = missionID;
    MissionID = missionID;
    oData.From = from;
    oData.To = to;
    oData.DataInput = dataInput;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/ChatAndData/InsertDataDriveData",
        data: oData,
        async: false,
        success: function (data) {
            if (team == "Volcano")
                clearVolcInputs();
            else if (team == "Hurricane")
                clearHurcInputs();
            else if (team == "Evacuation")
                clearEvacInputs();
            else if (team = "MedComm")
                $("#medcommModal").modal("hide")
            GetDataDriveData(parseInt(missionID, 10));
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ": " + errorThrown + ", " + jqXHR.responseText)
        }
    })
}

function disconnectNotice() {
    alert('You have been disconnected from Mission Control.');
}

//#endregion Chat

//#region Data Push

function setDataDriveOnLoad(missionID, teamName, currentRecord, currentTimerInterval) {
    if (teamName == "Volcano") {
        volcObject.CurrentRecord = currentRecord;
        volcObject.DataInterval = currentTimerInterval;
        updateData(missionID, volcObject);
    }
    if (teamName == "Hurricane") {
        hurcObject.CurrentRecord = currentRecord;
        hurcObject.DataInterval = currentTimerInterval;
        updateData(missionID, "Hurricane", hurcObject);
    }
    if (teamName == "MedComm") {
        medcObject.CurrentRecord = currentRecord;
        medcObject.DataInterval = currentTimerInterval;
        updateData(missionID, medcObject);
    }
}

function getDataDrive(missionID, teamName, teamID, interval) {
    var record = 0;
    if (teamName == "Volcano")
        record = volcObject.CurrentRecord;
    if (teamName == "Hurricane")
        record = hurcObject.CurrentRecord;
    if (teamName == "MedComm")
        record = medcObject.CurrentRecord;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/GetDataDrive",
        data: { pMissionID: missionID, teamName, pTeamID: teamID },
        cache: false,
        success: function (result) {
            if (result == null) {
                // Insert the initial record.
                insertUpdateDataPush(missionID, teamName, teamID, record, interval);
            } else {

                if (teamName == "Volcano") {
                    volcObject.CurrentRecord = result.currentRecord;
                    alertVolcTimer();
                }
                if (teamName == "Hurricane") {
                    hurcObject.CurrentRecord = result.currentRecord;
                    alertHurcTimer();
                }
                if (teamName == "MedComm") {
                    medcObject.CurrentRecord = result.currentRecord;
                    alertMedCommTimer();
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    });
}

function insertUpdateDataPush(missionID, teamName, teamID, currentRecord, currentTimerInterval) {
    if (teamID <= 0) {
        return 0;
    }
    var oDataDrive = new Object();
    oDataDrive.MissionID = missionID;
    oDataDrive.TeamName = teamName;
    oDataDrive.TeamID = teamID;
    oDataDrive.CurrentRecord = currentRecord;
    oDataDrive.CurrentTimerInterval = currentTimerInterval;
    MissionID = missionID;
    if (currentRecord == 1) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/ChatAndData/InsertDataDrive",
            data: oDataDrive,
            success: function (result) {
                if (teamName == "Volcano") {
                    getCurrentGMT(missionID);
                    volcObject.CurrentRecord = 1;
                    updateData(missionID, volcObject);
                }
                else if (teamName == "Hurricane") {
                    getCurrentADV(missionID);
                    hurcObject.CurrentRecord = 1;
                    updateData(missionID, hurcObject);
                }
                else if (teamName == "MedComm") {
                    getCurrentMCGMT(missionID);
                    medcObject.CurrentRecord = 1;
                    updateData(missionID, medcObject);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus + ": " + errorThrown + jqXHR.responseText)
            }
        });
        //Insert record
    } else {
        $.ajax({
            type: "PUT",
            dataType: "json",
            url: "/ChatAndData/UpdateDataDrive",
            data: oDataDrive,
            success: function (result) {
                if (teamName == "Volcano") {
                    getCurrentGMT(missionID);
                    updateData(MissionID, volcObject);
                }
                else if (teamName == "Hurricane") {
                    getCurrentADV(missionID);
                    updateData(MissionID, hurcObject);
                }
                else if (teamName == "MedComm") {
                    getCurrentMCGMT(missionID);
                    updateData(MissionID, medcObject);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus + ": " + errorThrown + jqXHR.responseText)
            }
        });
        //Update record
    }
}

//#endregion Data Push

//#region Tabs

$(".nav-link").click(function () {
    $(".nav-item").removeClass("active");
    $(".nav-item").removeClass("show");
});

//#endregion Tabs

//#region Clear Inputs

function clearVolcInputs() {
    $("#GMT").prop("selectedIndex", -1);
    $("#HourlyVT").val(null);
    $("#CumulVT").val(null);
    $("#ProjVT").val(null);
    $("#HourlyRF").val(null);
    $("#CumulRF").val(null);
    $("#ProjRF").val(null);
    $("#TSeismic").val(null);
}

function clearHurcInputs() {
    $("#ADV").prop("selectedIndex", -1);
    $("#Lat").val(null);
    $("#Lng").val(null);
    $("#Cat").prop("selectedIndex", -1);
    $("#DistTrav").val(null);
    $("#Speed").val(null);
    $("#Direction").val(null);
    $("#DistIsl").val(null);
    $("#ETA").val(null);
}

function clearEvacInputs() {
    $("#evacFrom").val(null);
    $("#evacPop").val(null);
    $("#evacTrans").val(null);
    $("#evacTo").val(null);
    $("#evacComplete").prop("checked", false);
}

function clearMedCommInputs() {
    $("#distributiontype").prop("selectedIndex", -1);
    $("#headline").val(null);
    $("#context").val(null);
    $("#situation").val(null);
    $("#remedy").val(null);
    $("#knowledge").val(null);
    $("#future").val(null);
    $("#moreinfo").val(null);
    $("#bulletinbody").val(null);
}

//#endregion Clear Inputs

//#region Evacuation Map Interface

function changeMap(btn) {
    //This is messy, but is necessary because the state of the button passed in is inverse to the
    //state that I want.  So, each button has it's own set of statements to determine which map to display.

    var imgSrc = null;

    var blnTopoActive = $("#btnTopo").hasClass("active");
    var blnPopActive = $("#btnPop").hasClass("active");
    var blnRoadActive = $("#btnRoad").hasClass("active");

    switch (btn) {
        case "btnTopo": //Topographic is inverted
            if (!blnTopoActive && blnPopActive && blnRoadActive)            //o o o
                imgSrc = "../assets/images/outline_pop_phys.png";
            else if (!blnTopoActive && !blnPopActive && !blnRoadActive)     //o x x
                imgSrc = "../assets/images/physmap.png";
            else if (!blnTopoActive && blnPopActive && !blnRoadActive)      //o o x
                imgSrc = "../assets/images/pop_phys.png";
            else if (!blnTopoActive && !blnPopActive && blnRoadActive)      //o x o
                imgSrc = "../assets/images/outline_pop_phys.png";   //TODO: need a map for outline_phys
            else if (blnTopoActive && blnPopActive && blnRoadActive)        //x o o
                imgSrc = "../assets/images/outline_pop.png";
            else if (blnTopoActive && !blnPopActive && blnRoadActive)       //x x o
                imgSrc = "../assets/images/outline.png";
            else if (blnTopoActive && blnPopActive && !blnRoadActive)       //x o x
                imgSrc = "../assets/images/popdense.png";
            else if (blnTopoActive && !blnPopActive && !blnRoadActive)      //x x x
                imgSrc = null;
            break;
        case "btnPop":  //Pop Density is inverted
            if (blnTopoActive && !blnPopActive && blnRoadActive)            //o o o
                imgSrc = "../assets/images/outline_pop_phys.png";
            else if (blnTopoActive && blnPopActive && !blnRoadActive)       //o x x
                imgSrc = "../assets/images/physmap.png";
            else if (blnTopoActive && !blnPopActive && !blnRoadActive)      //o o x
                imgSrc = "../assets/images/pop_phys.png";
            else if (blnTopoActive && blnPopActive && blnRoadActive)        //o x o
                imgSrc = "../assets/images/outline_pop_phys.png";   //TODO: need a map for outline_phys
            else if (!blnTopoActive && !blnPopActive && blnRoadActive)      //x o o
                imgSrc = "../assets/images/outline_pop.png";
            else if (!blnTopoActive && blnPopActive && blnRoadActive)       //x x o
                imgSrc = "../assets/images/outline.png";
            else if (!blnTopoActive && !blnPopActive && !blnRoadActive)     //x o x
                imgSrc = "../assets/images/popdense.png";
            else if (!blnTopoActive && blnPopActive && !blnRoadActive)      //x x x
                imgSrc = null;
            break;
        case "btnRoad": //Roadways is inverted
            if (blnTopoActive && blnPopActive && !blnRoadActive)            //o o o
                imgSrc = "../assets/images/outline_pop_phys.png";
            else if (blnTopoActive && !blnPopActive && blnRoadActive)       //o x x
                imgSrc = "../assets/images/physmap.png";
            else if (blnTopoActive && blnPopActive && blnRoadActive)        //o o x
                imgSrc = "../assets/images/pop_phys.png";
            else if (blnTopoActive && !blnPopActive && !blnRoadActive)      //o x o
                imgSrc = "../assets/images/outline_pop_phys.png";   //TODO: need a map for outline_phys
            else if (!blnTopoActive && blnPopActive && !blnRoadActive)      //x o o
                imgSrc = "../assets/images/outline_pop.png";
            else if (!blnTopoActive && !blnPopActive && !blnRoadActive)     //x x o
                imgSrc = "../assets/images/outline.png";
            else if (!blnTopoActive && blnPopActive && blnRoadActive)       //x o x
                imgSrc = "../assets/images/popdense.png";
            else if (!blnTopoActive && !blnPopActive && blnRoadActive)      //x x x
                imgSrc = null;
            break;
    }

    // set the map image source
    $("#theMap").attr("src", imgSrc);

    if (imgSrc != null)
        $("#canvas").removeClass("d-none");
    else
        $("#canvas").addClass("d-none");
}

//#endregion Evacuation Map Interface

function showOnlineOffline(user, connected) {
    switch (user) {
        case "Communications":
            if (connected == true) {
                document.getElementById("badgeComm").classList.remove("badge-danger");
                document.getElementById("badgeComm").classList.add("badge-success");
            } else {
                document.getElementById("badgeComm").classList.remove("badge-success");
                document.getElementById("badgeComm").classList.add("badge-danger");
            }
            break;
        case "Volcano":
            if (connected == true) {
                document.getElementById("badgeVolc").classList.remove("badge-danger");
                document.getElementById("badgeVolc").classList.add("badge-success");
            } else {
                document.getElementById("badgeVolc").classList.remove("badge-success");
                document.getElementById("badgeVolc").classList.add("badge-danger");
            }
            break;
        case "Hurricane":
            if (connected == true) {
                document.getElementById("badgeHurc").classList.remove("badge-danger");
                document.getElementById("badgeHurc").classList.add("badge-success");
            } else {
                document.getElementById("badgeHurc").classList.remove("badge-success");
                document.getElementById("badgeHurc").classList.add("badge-danger");
            }
            break;
        case "Evacuation":
            if (connected == true) {
                document.getElementById("badgeEvac").classList.remove("badge-danger");
                document.getElementById("badgeEvac").classList.add("badge-success");
            } else {
                document.getElementById("badgeEvac").classList.remove("badge-success");
                document.getElementById("badgeEvac").classList.add("badge-danger");
            }
            break;
        case "MedComm":
            if (connected == true) {
                document.getElementById("badgeMedComm").classList.remove("badge-danger");
                document.getElementById("badgeMedComm").classList.add("badge-success");
            } else {
                document.getElementById("badgeMedComm").classList.remove("badge-success");
                document.getElementById("badgeMedComm").classList.add("badge-danger");
            }
    }
}

$("#btnOpenMissionOptionsModal").on("click", function (e) {
    // determine whether or not the CLC Center can run a mission that has access to the Med Comm interfaces.
    // hide it until we find out if the mission can have Med Comm.
    $("#ScriptID option").each(function () {
        var title = $(this).text();
        var n = title.indexOf("MEDCOM");
        if (n >= 0)
            $(this).addClass("d-none");
    });
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/AllowsMedComm",
        data: { clcCenter: $("#CLCCenter").val() },
        cache: false,
        success: function (result) {
            if (result == false) {
                $("#ScriptID option").each(function () {
                    var title = $(this).text();
                    var n = title.indexOf("MEDCOM");
                    if (n >= 0)
                        $(this).addClass("d-none");
                });
            }
            else {
                $("#ScriptID option").each(function () {
                    var title = $(this).text();
                    var n = title.indexOf("MEDCOM");
                    if (n >= 0)
                        $(this).removeClass("d-none");
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    })
});

$("#btnSendMissionOptionsSelection").click(function () {
    var ending = $("#MissionEnding").val();
    var script = $("#ScriptID").val();

    $("#myEnding").val(ending);
    $("#myScript").val(script);

    $("#missionOptionsModal").modal("hide");
})

$("#btnOpenTeamModal").on("click", function (e) {
    // determine whether or not the Med Comm team displays in the modal drop down based on which script is being used.
    // hide it until we find out if the mission includes Med Comm.
    $("#TeamName option").each(function () {
        if ($(this).val() == 4)
            $(this).addClass("d-none");
    });
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/HasMedComTeam",
        data: { booth: $("#password").val() },
        success: function (result) {
            if (result == false) {
                $("#TeamName option").each(function () {
                    if ($(this).val() == 4) //Med Comm team
                        $(this).addClass("d-none");
                });
            }
            else {
                $("#TeamName option").each(function () {
                    if ($(this).val() == 4) //Med Comm team
                        $(this).removeClass("d-none");
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    })
});

$("#btnSendTeamSelection").click(function () {
    var value = $("#TeamName").val();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/GetTeam",
        data: { teamID: value },
        cache: false,
        success: function (result) {
            console.log(result);
        },
        error: function (jqXHT, textStatus, errorThrown) {
            alert(textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    });

    $("#myTeamName").val(value);
    $("#teamModal").modal("hide");
});

$("#btnOpenBulletinModal").on("click", function (e) {
    $("#medcommModal").modal("show");
})

$("#context, #situation, #remedy, #knowledge, #future, #moreinfo").on("blur", function () {
    var body = $("#context").val() + '\n' +
        $("#situation").val() + '\n' +
        $("#remedy").val() + '\n' +
        $("#knowledge").val() + '\n' +
        $("#future").val() + '\n' +
        $("#moreinfo").val();

    $("#bulletinbody").val(body);
})

$("#medcommModal").on("hidden.bs.modal", function () {
    clearMedCommInputs();
})

$("#teamLoginSubmit").on("click", function () {
    password = $("#password").val();
})

function InitInterface(password) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ChatAndData/HasMedComTeam",
        data: { booth: password },
        cache: false,
        success: function (result) {
            if (result == false) {
                if (document.getElementById("MedCommDataDriveData")) {
                    document.getElementById("MedCommDataDriveData").classList.add("d-none")
                }
            }
            else {
                if (document.getElementById("MedCommDataDriveData")) {
                    document.getElementById("MedCommDataDriveData").classList.remove("d-none")
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus + ": " + errorThrown + jqXHR.responseText);
        }
    })
}