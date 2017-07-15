var ctx;
var speed;
var increment;
var intervalID;
function init() {
    var canvas = document.getElementById('canvas');
    if (canvas.getContext) {
        ctx = canvas.getContext("2d");
        drawFigure();
    }
}

function startDraw() {
    clearInterval(intervalID);
    speed = 0;
    increment = 0.01;
    var radios = document.getElementsByName('Fradio');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            intervalID = setInterval(drawTransformation, 15, radios[i].value);
            break;
        }
    }
}

function drawTransformation(transformation) {
    ctx.clearRect(0, 0, 500, 500);

    if (speed == 0) {
        drawFigure();
    } else {
        switch (transformation) {
            case "1":
                ctx.save();
                ctx.rotate(Math.PI * 2 - (Math.PI / 2 * speed));
                ctx.scale(1 - 1.5 * speed, 1 - 0.5 * speed);

               /* ctx.scale(1 - 0.5 * speed, 1 - 1.5 * speed);
                ctx.rotate( Math.PI * 3 / 2 * speed);*/
                drawFigure();
                ctx.restore();
                break;
            case "2":
                ctx.save();
                ctx.translate(375 * speed, 125 * speed);
                ctx.scale(1 - 1.25 * speed, 1 - 0.75 * speed);
                drawFigure();
                ctx.restore();
                break;
            case "3":
                ctx.save();
                ctx.translate(0, 250 * speed);
                ctx.scale(1 - 0.5 * speed, 1 - 0.5 * speed);
                drawFigure();
                ctx.restore();
                break;
            case "4":
                ctx.save();
                ctx.translate(500 * speed, 250 * speed);
                ctx.scale(1 - 0.5 * speed, 1 - 0.5 * speed);
                ctx.rotate(Math.PI / 2 * speed);
                drawFigure();
                ctx.restore();
                break
        }
    }

    speed += increment;
    //alert(transNr);
    if (speed > 1.01) {
        clearInterval(intervalID);
    }
}

function drawFigure() {
    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(100, 0);
    ctx.lineTo(100, 325);
    ctx.lineTo(500, 325);
    ctx.lineTo(500, 500);
    ctx.lineTo(0, 500);
    ctx.fillStyle = "black";
    ctx.fill();
}