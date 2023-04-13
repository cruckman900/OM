const canvas = document.getElementById("canvas");
canvas.width = 480;
canvas.height = 710;
const ctx = canvas.getContext("2d");

var updatedCircles;

function redrawCircles(item) {
    updateCircle(item.x, item.y, item.circleSize, item.color);
}

// Create the city dots and implement a clicked function to open
// a modal dialog to enter data and partially or completely evac a city
Circle = function (name, ctx, x, y, color, circleSize) {
    ctx.beginPath();
    ctx.arc(x, y, circleSize, 0, Math.PI * 2, true);
    ctx.strokeStyle = "#000000";
    ctx.fillStyle = color;
    ctx.lineWidth = 1;
    ctx.fill();
    ctx.stroke();
    ctx.closePath();
    this.clicked = function (x, y, circleSize) {
        $(".modal-body #evacFrom").val(name);
        $(".modal-body #x").val(x);
        $(".modal-body #y").val(y);
        $(".modal-body #circleSize").val(circleSize)
        $("#evacModal").modal("show");
    }
};

// Redraw the circle to indicate evacuation status
function updateCircle(x, y, circleSize, hexColor) {
    ctx.beginPath();
    ctx.arc(x, y, circleSize, 0, Math.PI * 2, true);
    ctx.strokeStyle = "#000000";
    ctx.fillStyle = hexColor;
    ctx.lineWidth = 1;
    ctx.fill();
    ctx.stroke();
    $("#evacModal").modal("hide");
}

function getMousePos(canvas, evt) {
    // Get the mouse position relative to the canvas
    var rect = canvas.getBoundingClientRect();
    return {
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    };
}

window.onload = function () {
    // Initialize the buttons
    // default color for the city dots
    var color = "#2E9AFE";

    var Rendezvous = new Circle("Rendezvous", ctx, 220, 122, color, 3);
    var DavyHill = new Circle("Davy Hill", ctx, 190, 189, color, 3);
    var Geralds = new Circle("Gerald's", ctx, 229, 182, color, 3);
    var StPeters = new Circle("St. Peter's", ctx, 146, 259, color, 3);
    var StJohns = new Circle("St. John's", ctx, 236, 231, color, 3);
    var OldTowne = new Circle("Old Towne", ctx, 91, 371, color, 3);
    var Salem = new Circle("Salem", ctx, 137, 358, color, 3);
    var Trants = new Circle("Trant's", ctx, 362, 339, color, 3);
    var Harris = new Circle("Harris", ctx, 313, 394, color, 3);
    var CorkHill = new Circle("Cork Hill", ctx, 144, 429, color, 3);
    var Weekes = new Circle("Weekes", ctx, 161, 443, color, 3);
    var Streatham = new Circle("Streatham", ctx, 256, 429, color, 3);
    var FerrellsYard = new Circle("Farrell's Yard", ctx, 271, 428, color, 3);
    var Tuitts = new Circle("Tuitt's", ctx, 353, 416, color, 3);
    var Richmond = new Circle("Richmond", ctx, 122, 477, color, 3);
    var LongGround = new Circle("Long Ground", ctx, 354, 457, color, 3);
    var Plymouth = new Circle("Plymouth", ctx, 129, 530, color, 5);
    var Gages = new Circle("Gages", ctx, 194, 488, color, 3);
    var BrodericksEstate = new Circle("Broderick's Estate", ctx, 188, 547, color, 3);
    var Trials = new Circle("Trials", ctx, 176, 561, color, 3);
    var Gingoes = new Circle("Gingoes", ctx, 178, 584, color, 3);
    var StPatricks = new Circle("St. Patrick's", ctx, 216, 603, color, 3);
    var FergusMountEstate = new Circle("Fergus Mount Estate", ctx, 273, 599, color, 3);

    // Change cursor on mouse over
    $("#canvas").on("mousemove", function (e) {
        var mousePos = getMousePos(canvas, e);
        var x = mousePos.x;
        var y = mousePos.y;

        if (Math.pow(x - 220, 2) + Math.pow(y - 122, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 190, 2) + Math.pow(y - 189, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 229, 2) + Math.pow(y - 182, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 146, 2) + Math.pow(y - 259, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 236, 2) + Math.pow(y - 231, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 91, 2) + Math.pow(y - 371, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 137, 2) + Math.pow(y - 358, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 362, 2) + Math.pow(y - 339, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 313, 2) + Math.pow(y - 394, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 144, 2) + Math.pow(y - 429, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 161, 2) + Math.pow(y - 443, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 256, 2) + Math.pow(y - 429, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 271, 2) + Math.pow(y - 428, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 353, 2) + Math.pow(y - 416, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 122, 2) + Math.pow(y - 477, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 354, 2) + Math.pow(y - 457, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 129, 2) + Math.pow(y - 530, 2) < Math.pow(5, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 194, 2) + Math.pow(y - 488, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 188, 2) + Math.pow(y - 547, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 176, 2) + Math.pow(y - 561, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 178, 2) + Math.pow(y - 584, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 216, 2) + Math.pow(y - 603, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else if (Math.pow(x - 273, 2) + Math.pow(y - 599, 2) < Math.pow(3, 2))
            $("#canvas").css("cursor", "pointer");
        else
            $("#canvas").css("cursor", "auto");
    });

    // On click, subscribe to click event handler described in the Circle function
    $("#canvas").on("click", function (e) {
        var mousePos = getMousePos(canvas, e);
        var x = mousePos.x;
        var y = mousePos.y;
        if (Math.pow(x - 220, 2) + Math.pow(y - 122, 2) < Math.pow(3, 2))
            Rendezvous.clicked(220, 122, 3);
        else if (Math.pow(x - 190, 2) + Math.pow(y - 189, 2) < Math.pow(3, 2))
            DavyHill.clicked(190, 189, 3);
        else if (Math.pow(x - 229, 2) + Math.pow(y - 182, 2) < Math.pow(3, 2))
            Geralds.clicked(229, 182, 3);
        else if (Math.pow(x - 146, 2) + Math.pow(y - 259, 2) < Math.pow(3, 2))
            StPeters.clicked(146, 259, 3);
        else if (Math.pow(x - 236, 2) + Math.pow(y - 231, 2) < Math.pow(3, 2))
            StJohns.clicked(236, 231, 3);
        else if (Math.pow(x - 91, 2) + Math.pow(y - 371, 2) < Math.pow(3, 2))
            OldTowne.clicked(91, 371, 3);
        else if (Math.pow(x - 137, 2) + Math.pow(y - 358, 2) < Math.pow(3, 2))
            Salem.clicked(137, 358, 3);
        else if (Math.pow(x - 362, 2) + Math.pow(y - 339, 2) < Math.pow(3, 2))
            Trants.clicked(362, 339, 3);
        else if (Math.pow(x - 313, 2) + Math.pow(y - 394, 2) < Math.pow(3, 2))
            Harris.clicked(313, 394, 3);
        else if (Math.pow(x - 144, 2) + Math.pow(y - 429, 2) < Math.pow(3, 2))
            CorkHill.clicked(144, 429, 3);
        else if (Math.pow(x - 161, 2) + Math.pow(y - 443, 2) < Math.pow(3, 2))
            Weekes.clicked(161, 443, 3);
        else if (Math.pow(x - 256, 2) + Math.pow(y - 429, 2) < Math.pow(3, 2))
            Streatham.clicked(256, 429, 3);
        else if (Math.pow(x - 271, 2) + Math.pow(y - 428, 2) < Math.pow(3, 2))
            FerrellsYard.clicked(271, 428, 3);
        else if (Math.pow(x - 353, 2) + Math.pow(y - 416, 2) < Math.pow(3, 2))
            Tuitts.clicked(353, 416, 3);
        else if (Math.pow(x - 122, 2) + Math.pow(y - 477, 2) < Math.pow(3, 2))
            Richmond.clicked(122, 477, 3);
        else if (Math.pow(x - 354, 2) + Math.pow(y - 457, 2) < Math.pow(3, 2))
            LongGround.clicked(354, 457, 3);
        else if (Math.pow(x - 129, 2) + Math.pow(y - 530, 2) < Math.pow(5, 2))
            Plymouth.clicked(129, 530, 5);
        else if (Math.pow(x - 194, 2) + Math.pow(y - 488, 2) < Math.pow(3, 2))
            Gages.clicked(194, 488, 3);
        else if (Math.pow(x - 188, 2) + Math.pow(y - 547, 2) < Math.pow(3, 2))
            BrodericksEstate.clicked(188, 547, 3);
        else if (Math.pow(x - 176, 2) + Math.pow(y - 561, 2) < Math.pow(3, 2))
            Trials.clicked(176, 561, 3);
        else if (Math.pow(x - 178, 2) + Math.pow(y - 584, 2) < Math.pow(3, 2))
            Gingoes.clicked(178, 584, 3);
        else if (Math.pow(x - 216, 2) + Math.pow(y - 603, 2) < Math.pow(3, 2))
            StPatricks.clicked(216, 603, 3);
        else if (Math.pow(x - 273, 2) + Math.pow(y - 599, 2) < Math.pow(3, 2))
            FergusMountEstate.clicked(273, 599, 3);
        //else
        //    alert(x + ', ' + y);
    });
};