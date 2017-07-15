var rightDown = false; var leftDown = false;
var topDown = false; var bottomDown = false;
var speedUp = false; var speedDown = false;
var movementSpeed = 5;

function draw() {
    var ctx;
    var xSign = 0;  var ySign = 0;

    var currentAngle = 0;
    var baseAngleSpeed = Math.PI / 128;

    var canvas = document.getElementById('canvas');
    if (canvas.getContext) {
        ctx = canvas.getContext("2d");
        var largeGear = new gear(0, 0, 216, 50, 10, 72, "Purple");
        var mediumGear = new gear(229, 229, 98, 15, 10, 36, "Green");
        var smallGear = new gear(400, 100, 8, 5, 10, 6, "Red");

        return setInterval(spinGears, 10);
    }

    function gear(centerX, centerY, gearRadius, holeRadius, spikeSize, spikeQuantity, color) {
        this.centerX = centerX;
        this.centerY = centerY;
        this.radius = gearRadius;
        this.holeRadius = holeRadius;
        this.spikeSize = spikeSize;
        this.spikeQuantity = spikeQuantity;
        this.color = color;

        this.drawGear = function drawGear(centerX, centerY) {
            if (centerX === undefined) {
                centerX = this.centerX;
            }

            if (centerY === undefined) {
                centerY = this.centerY;
            }

            ctx.beginPath();
            ctx.moveTo(centerX, centerY - gearRadius - spikeSize);

            for (i = 0; i <= spikeQuantity; i++) {
                //Draw spike head
                ctx.lineTo(centerX - (gearRadius + spikeSize) * Math.sin(Math.PI * 2 / spikeQuantity * i), centerY - (gearRadius + spikeSize) * Math.cos(Math.PI * 2 / spikeQuantity * i));
                //Draw spike right bottom
                ctx.lineTo(centerX - (gearRadius) * Math.sin(Math.PI * 2 / spikeQuantity * i + Math.PI * 2 / spikeQuantity / 2), centerY - (gearRadius) * Math.cos(Math.PI * 2 / spikeQuantity * i + Math.PI * 2 / spikeQuantity / 2));
            }

            ctx.closePath();
            ctx.fillStyle = color;
            ctx.fill();

            ctx.beginPath();
            ctx.fillStyle = "White";
            ctx.arc(centerX, centerY, holeRadius, 0, Math.PI * 2, true);
            ctx.fill();
        }

        this.drawRotatingGear = function drawRotatingGear(speed, centerX, centerY) {
            if (centerX === undefined) {
                centerX = this.centerX;
            }

            if (centerY === undefined) {
                centerY = this.centerY;
            }

            ctx.save();

            ctx.translate(this.centerX, this.centerY);
            ctx.rotate(speed);
            this.drawGear(0, 0);

            ctx.restore();
        }
        
        this.checkCollision = function checkCollision(other) {
            if (this.getHypotenuse(other) < this.getCollisionRange(other))
                return true;
        }

        this.collide = function collide(other) {
            xSign = this.centerX < other.centerX ? -1 : 1;
            ySign = this.centerY < other.centerY ? -1 : 1;

            var degree1 = Math.acos(this.getCos(other)) * 180 / Math.PI;
            var threshold = 360 / other.spikeQuantity;
            var otherOffset = this.getOffset(degree1, threshold, true);
            var otherOffset2 = degree1 % threshold;

            var degree2 = currentAngle * 180 / Math.PI;
            threshold = 360 / this.spikeQuantity;
            var thisOffsetFromOrigin = this.getOffset(degree2, threshold, true);

            var degree3 = Math.asin(this.getCos(other)) * 180 / Math.PI;
            degree3 = 360 - degree1;
            threshold = 360 / this.spikeQuantity;
            var thisOffset = this.getOffset(degree3, threshold, false);
            var thisOffset2 = degree3 % threshold + threshold / 2;

            //currentAngle = 0;

            //otherOffset = 0;
            thisOffsetFromOrigin = 0;
            //thisOffset = 0;

            var totalThisOffset = thisOffsetFromOrigin + thisOffset;

            /*logInfo(
                '<br/> X : ' + this.getXDifference(other) + ' Y : ' + this.getYDifference(other) +
                '<br/> Collision range: ' + this.getCollisionRange(other) +
                '<br/> Hypotenuse: ' + this.getHypotenuse(other) +

                '<br/>' +

                '<br/> Original degree: ' + currentAngle * 180 / Math.PI +
                '<br/> degree 1: ' + degree1 +
                '<br/> degree 3: ' + degree3 +

                '<br/>' +

                '<br/> offsetOther 1: ' + otherOffset * 180 / Math.PI + ' offsetOther 2: ' + otherOffset2 +
                '<br/> thisOffset 1: ' + thisOffset * 180 / Math.PI + ' thisOffset 2: ' + thisOffset2 +

                '<br/>' +

                '<br/> otherOffset ' + otherOffset * 180 / Math.PI +
                '<br/> thisOffsetFromOrigin ' + thisOffsetFromOrigin * 180 / Math.PI +
                '<br/> thisOffset ' + thisOffset * 180 / Math.PI +

                '<br/>' +

                '<br/> Big ' + (currentAngle / 12 - otherOffset / 2) * 180 / Math.PI +
                '<br/> Medium ' + (-currentAngle / 6 + otherOffset) * 180 / Math.PI +
                '<br/> Small ' + (currentAngle + totalThisOffset) * 180 / Math.PI);*/

            if (other === largeGear) {
                largeGear.drawRotatingGear(-currentAngle / 12 + otherOffset);
                mediumGear.drawRotatingGear(currentAngle / 6 - otherOffset * 2);
                smallGear.drawRotatingGear(currentAngle - totalThisOffset * xSign);
            } else if (other === mediumGear) {
                largeGear.drawRotatingGear(currentAngle / 12 - otherOffset / 2);
                mediumGear.drawRotatingGear(-currentAngle / 6 + otherOffset);
                smallGear.drawRotatingGear(currentAngle - totalThisOffset * xSign);
            }
        }

        this.getXDifference = function getXDifference(other) {
            return this.centerX - other.centerX;
        }

        this.getYDifference = function getYDifference(other) {
            return this.centerY - other.centerY;
        }

        this.getHypotenuse = function getHypotenuse(other) {
            return Math.sqrt(Math.pow(this.getXDifference(other), 2) 
                + Math.pow(this.getYDifference(other), 2));
        }

        this.getSin = function getSin(other) {
            return  this.centerX != other.centerX ? this.getXDifference(other) / this.getHypotenuse(other) : 1;
        }

        this.getCos = function getCos(other) {
            return this.centerX != other.centerX ? this.getYDifference(other) / this.getHypotenuse(other) : 1;
        }

        this.getCollisionRange = function getCollisionRange(other) {
            return this.radius + other.radius + (this.spikeSize + other.spikeSize) / 3 * 2;
        }

        this.getOffset = function getOffset(degree, threshold, toSpike) {
            var sign = degree > 0 ? 1 : -1;

            var closestDegree = this.getClosestDegree(degree, threshold, toSpike);

            if (!toSpike)
                closestDegree += threshold / 2;
            var offset = closestDegree - degree;

            return offset / 180 * Math.PI;
        }

        this.getClosestDegree = function getClosestDegree(degree, threshold) {
            var sign = degree > 0 ? 1 : -1;
            var currentDegree = 0;

            while (currentDegree < Math.abs(degree))
                currentDegree += threshold;

            return (currentDegree - Math.abs(degree)) < Math.abs(currentDegree - threshold - Math.abs(degree)) ? currentDegree * sign : (currentDegree - threshold) * sign;
        }
    }
				
    function spinGears() {
        
        logInfo();
        ctx.clearRect(0, 0, 500, 500);

        drawBorder();
        checkButtons(smallGear);

        if (smallGear.checkCollision(largeGear)) {
            smallGear.collide(largeGear);
        } else if (smallGear.checkCollision(mediumGear)) {
            smallGear.collide(mediumGear);
        } else {
            cleanSigns();
            largeGear.drawGear();
            mediumGear.drawGear();
            smallGear.drawRotatingGear(currentAngle);
        }

        currentAngle = (currentAngle + baseAngleSpeed) % (2 * Math.PI);
    }
				
    function drawBorder() {
        ctx.beginPath();
        ctx.moveTo(0,0);
        ctx.lineTo(500,0);
        ctx.lineTo(500,500);
        ctx.lineTo(0,500);
        ctx.closePath();
        ctx.stroke(); 
    }
				
    function logInfo(extraMessage) {
        var div = document.getElementById('coordinates');
        div.innerHTML = 'X: ' + smallGear.centerX + ' Y: ' + smallGear.centerY
        + '<br/> signX: ' + xSign + ' signY: ' + ySign;

        if (extraMessage !== undefined)
            div.innerHTML += extraMessage;
    }

    function checkButtons(smallGear) {
        if (rightDown && xSign != -1)
            smallGear.centerX += movementSpeed;
						
        if (leftDown && xSign != 1)
            smallGear.centerX -= movementSpeed;
						
        if (bottomDown && ySign != -1)
            smallGear.centerY += movementSpeed;
						
        if (topDown && ySign != 1)
            smallGear.centerY -= movementSpeed;
						
        if (speedUp)
            baseAngleSpeed += Math.PI / 128;
						
        if (speedDown)
            baseAngleSpeed -= Math.PI / 128;
    }

    function cleanSigns() {
        xSign = 0;
        ySign = 0;
    }
}
			
function onKeyDown(evt) {
    switch (evt.keyCode) {
        case 37: 
            leftDown = true;
            break;
        case 38: 
            topDown = true;
            break;
        case 39: 
            rightDown = true;
            break;
        case 40: 
            bottomDown = true;
            break;
        case 107: 
            speedUp = true;
            break;
        case 109: 
            speedDown = true;
            break;
						
        default: return; 
    }
}
			
function onKeyUp(evt) {
    switch (evt.keyCode) {
        case 37:
            leftDown = false;
            break;
        case 38:
            topDown = false;
            break;
        case 39:
            rightDown = false;
            break;
        case 40:
            bottomDown = false;
            break;
        case 107: 
            speedUp = false;
            break;
        case 109: 
            speedDown = false;
            break;
    }
}
				
window.addEventListener('keydown', onKeyDown);
window.addEventListener('keyup', onKeyUp);