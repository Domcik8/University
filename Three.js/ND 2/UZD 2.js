var test = true;
var color = "Yellow";

function draw() {
    var ctx;

    var canvas = document.getElementById('canvas');
    if (canvas.getContext) {
        ctx = canvas.getContext("2d");
        readRange();
    }

    function drawFractal(depth, setColor) {
        if (depth == 1) {
            drawFigure(color);
            return;
        }

        //dirbam su 1/4
        ctx.save();
        if (setColor)
            color = "Red";
        /*ctx.translate(250, 250);
        ctx.scale(-0.5, -0.5);*/
        ctx.rotate(Math.PI * 3 / 2);
        ctx.scale(-0.5, 0.5);
        
        drawFractal(depth - 1);
        ctx.restore();

        //dirbam su 2/4
        ctx.save();
        if (setColor)
            color = "Blue";
        ctx.translate(375, 125);
        ctx.scale(-0.25, 0.25);
        drawFractal(depth - 1);
        ctx.restore();

        //dirbam su 3/4
        ctx.save();
        if (setColor)
            color = "Green";
        ctx.translate(0, 250);
        ctx.scale(0.5, 0.5);
        drawFractal(depth - 1);
        ctx.restore();

        //dirbam su 4/4
        ctx.save();
        if (setColor)
            color = "Yellow";
        ctx.translate(500, 250);
        ctx.scale(0.5, 0.5);
        ctx.rotate(Math.PI / 2);
        drawFractal(depth - 1);
        ctx.restore();
    }

    function drawFigure(color) {
        ctx.beginPath();

        //Figura is kvadratu
        /*ctx.moveTo(0, 0);
        ctx.lineTo(250, 0);
        ctx.lineTo(250, 125);
        ctx.lineTo(250, 125);
        ctx.lineTo(375, 125);
        ctx.lineTo(375, 250);
        ctx.lineTo(500, 250);
        ctx.lineTo(500, 500);
        ctx.lineTo(0, 500);*/

        //L raide
        ctx.moveTo(0, 0);
        ctx.lineTo(100, 0);
        ctx.lineTo(100, 325);
        ctx.lineTo(500, 325);
        ctx.lineTo(500, 500);
        ctx.lineTo(0, 500);

        //Kvadratas
        //ctx.rect(0, 0, 500, 500);

       // ctx.rect(0, 0, width, height);
        if (test) {
            ctx.fillStyle = color;
        } else {
            ctx.fillStyle = "black";
        }
        ctx.fill();
    }

    function drawBorder() {
        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(500, 0);
        ctx.lineTo(500, 500);
        ctx.lineTo(0, 500);
        ctx.closePath();
        ctx.stroke();
    }

    function readRange() {
        ctx.clearRect(0, 0, 500, 500);
        drawBorder();

        var rangeHandle = document.getElementById("range");
        var div = document.getElementById('coordinates');
        div.innerHTML = 'Range: ' + rangeHandle.value;
        
        drawFractal(rangeHandle.value, true);
    }

    document.getElementById("range").addEventListener("change", readRange);
}