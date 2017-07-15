// once everything is loaded, we run our Three.js stuff.
$(function () {
    n = 5;
    size = 5;
    var bricks;
    var clouds;
    var crate;
    var stone;
    var stoneChildren;
    var water;
    var wood;
    var currentView = 1;
    var cameraUpPosition = 0;

	var stats = initStats();

	// create a scene, that will hold all our elements such as objects, cameras and lights.
	var scene = new THREE.Scene();

	// create a camera, which defines where we're looking at.
	var camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 0.1, 1000);

	// create a render and set the size
	var webGLRenderer = new THREE.WebGLRenderer();
	webGLRenderer.setClearColorHex(0xEEEEEE, 1.0);
	webGLRenderer.setSize(window.innerWidth, window.innerHeight);
	webGLRenderer.shadowMapEnabled = true;

    // axes helpedr
    // var axes = new THREE.AxisHelper( 50 );
    // scene.add(axes);

	var ambiLight = new THREE.AmbientLight(0x141414)
	scene.add(ambiLight);

	var light = new THREE.DirectionalLight();
	light.position.set(50, 0, 0);
	scene.add(light);

    // add spotlight for the shadows
	var spotLight = new THREE.SpotLight(0xffffff);
	spotLight.position.set(-40, 60, -10);
	spotLight.castShadow = true;
	scene.add(spotLight);

	var megaCube = createCubes();

	// add the output of the renderer to the html element
	$("#WebGL-output").append(webGLRenderer.domElement);
    var trackBallControls = new THREE.TrackballControls(camera, webGLRenderer.domElement);

	// call the render function
	var step = 0;

	var params = new function () {
	    this.cameraX = 50;
	    this.cameraY = 50;
	    this.cameraZ = 0;
	    this.cameraFov = 0;
	    this.cameraDistance = 0;
	    this.dollyZoom = 0;
	    this.sphereR = 20;
	    this.spherePoints = 750;
	    this.cameraSpeed = 1;

	    this.changeView = function () {
	        currentView = (currentView + 1) % 2;
	        params.update();
	    }

	    this.redraw = function () {
	        scene.remove(stone);
	        stone = generatePoints();
	        stone.position.x = 100;
	        scene.add(stone);
	    };

	    this.update = function () {
	        camera.position.x = params.cameraX;
	        camera.position.y = params.cameraY;
	        camera.position.z = params.cameraZ;

	        camera.fov = params.cameraFov;
	        camera.updateProjectionMatrix();
	        
	        var multiplier;
	        if (currentView == 0) {
	            multiplier = 1;
	        }
	        else
	            multiplier = -1;

	        var cameraDistance =  Math.sqrt((params.cameraX + params.dollyZoom) * (params.cameraX + params.dollyZoom)
                + (params.cameraY + params.dollyZoom) * (params.cameraY + params.dollyZoom));

	        camera.position.set(params.cameraX + params.dollyZoom * multiplier, params.cameraY + params.dollyZoom, params.cameraZ);

	        camera.fov = calculateFov(50, cameraDistance) + params.cameraFov;
	        camera.updateProjectionMatrix();
	    };
	}

	function calculateFov(width, cameraDistance) {
	    return 2 * Math.atan(width / (cameraDistance * 2)) * (180 / Math.PI);
	}

	var gui = new dat.GUI();
	/*gui.add(params, 'cameraX', -1000, 1000).onChange(params.update);
	gui.add(params, 'cameraY', -1000, 1000).onChange(params.update);
	gui.add(params, 'cameraZ', -1000, 1000).onChange(params.update);
	gui.add(params, 'cameraFov', -100, 100).onChange(params.update);
	gui.add(params, 'cameraDistance', -100, 100).onChange(params.update);*/
	gui.add(params, 'dollyZoom', 0, 200).onChange(params.update);
	gui.add(params, 'sphereR', 0, 25).onChange(params.update);
	gui.add(params, 'spherePoints', 0, 2500).onChange(params.update);
	gui.add(params, 'cameraSpeed', 0, 5);
	gui.add(params, 'changeView');
	gui.add(params, 'redraw');

	scene.add(megaCube);

	stone = generatePoints();
	stone.position.x = 100;
	scene.add(stone);

	var planeGeometry = new THREE.PlaneGeometry(300, 150);
	var plane = new createMesh(planeGeometry, "carpet.jpg");
	plane.receiveShadow = true;
	
	plane.rotation.x = -0.5 * Math.PI;
	plane.position.x = 50;
	plane.position.y = -50;
	plane.position.z = 0;
	scene.add(plane);

	var material = new THREE.MeshBasicMaterial({ color: 0xff0000, transparent: false });
	var sphereg = new THREE.SphereGeometry(0.2);
	var sphere = new THREE.Mesh(sphereg, material);
	scene.add(sphere);

	var distance = stone.position.x - megaCube.position.x;

	camera.lookAt(sphere.position);
	
    params.update();
	render();

	function generatePoints() {
	    // add 10 random spheres
	    var points = [];
	    for (var i = 0; i < params.spherePoints; i++) {
	        var randomX = -25 + Math.round(Math.random() * 50);
	        var randomY = -25 + Math.round(Math.random() * 50);
	        var randomZ = -25 + Math.round(Math.random() * 50);

	        if (randomX * randomX + randomY * randomY + randomZ * randomZ <= params.sphereR * params.sphereR)
	            points.push(new THREE.Vector3(randomX, randomY, randomZ));
	    }

	    if (points.length < 3) {
	        points.push(new THREE.Vector3(1, 0, 0));
	        points.push(new THREE.Vector3(0, 1, 0));
	        points.push(new THREE.Vector3(0, 0, 1));
	    }
        
	    stone = new THREE.Object3D();
	    var material = new THREE.MeshBasicMaterial({ color: 0xff0000, transparent: false });
	    points.forEach(function (point) {
	        var spGeom = new THREE.SphereGeometry(0.2);
	        var spMesh = new THREE.Mesh(spGeom, material);
	        spMesh.position = point;
	        stone.add(spMesh);
	    });

	    // use the same points to create a convexgeometry
	    var hullGeometry = new THREE.ConvexGeometry(points);

	    for (i = 0; i < hullGeometry.faceVertexUvs[0].length; i++) {
	        var vertice1 = new THREE.Vector2(
                coordToAngleRatio(hullGeometry.vertices[hullGeometry.faces[i].a]),
                (hullGeometry.vertices[hullGeometry.faces[i].a].y + params.sphereR) / (params.sphereR * 2)
            );

	        var vertice2 = new THREE.Vector2(
                coordToAngleRatio(hullGeometry.vertices[hullGeometry.faces[i].b]),
                (hullGeometry.vertices[hullGeometry.faces[i].b].y + params.sphereR) / (params.sphereR * 2)
            );

	        var vertice3 = new THREE.Vector2(
                coordToAngleRatio(hullGeometry.vertices[hullGeometry.faces[i].c]),
                (hullGeometry.vertices[hullGeometry.faces[i].c].y + params.sphereR) / (params.sphereR * 2)
            );

	        if (vertice1.x != checkCoordinate(vertice1.x, vertice2.x, vertice3.x))
	            vertice1.x = checkCoordinate(vertice1.x, vertice2.x, vertice3.x);
	        else if (vertice2.x != checkCoordinate(vertice2.x, vertice1.x, vertice3.x))
	            vertice2.x = checkCoordinate(vertice2.x, vertice1.x, vertice3.x);
            else if (vertice3.x != checkCoordinate(vertice3.x, vertice2.x, vertice1.x))
                vertice3.x = checkCoordinate(vertice3.x, vertice2.x, vertice1.x);

	        if (vertice1.y != checkCoordinate(vertice1.y, vertice2.y, vertice3.y))
	            vertice1.y = checkCoordinate(vertice1.y, vertice2.y, vertice3.y);
	        else if (vertice2.y != checkCoordinate(vertice2.y, vertice1.y, vertice3.y))
	            vertice2.y = checkCoordinate(vertice2.y, vertice1.y, vertice3.y);
	        else if (vertice3.y != checkCoordinate(vertice3.y, vertice2.y, vertice1.y))
	            vertice3.y = checkCoordinate(vertice3.y, vertice2.y, vertice1.y);


	        hullGeometry.faceVertexUvs[0][i] = [vertice1, vertice2, vertice3];
	    }

	    //stone.add(createMesh(hullGeometry, "chess.png"));
	    stone.add(createMesh(hullGeometry, "chess2.jpg"));
	    //stone.add(createMesh(hullGeometry, "chess3.gif"));
	    return stone;
	}

	function checkCoordinate(x, y, z) {
	    if (x - y > 0.4 && x - z > 0.4)
	        return 1 - x;
	    else if (y - x > 0.4 && z - x > 0.4)
	        return 1 - x;
	    return x;
    }

	function coordToAngleRatio(coordinate) {
	    var atan = Math.atan2(coordinate.x, coordinate.z);

	    if (atan < 0) {
	        atan = /*atan +*/ (Math.PI + atan);
	    }

	    atan = atan + atan;

	    return atan / (Math.PI * 2);
	}

	function createCubes() {
	    createImgVectors();
	    var megaCube = new THREE.Object3D();

	    for (i = 0; i < n; i++)
	        for (j = 0; j < n; j++)
	            for (k = 0; k < n; k++)
	                if (!skipPosition(i, j, k)) {
	                    var cube = createCube(i, j, k);
	                    megaCube.add(cube);
	                }
	    return megaCube;
	}

	function skipPosition(x, y, z) {
	    if (x == 0 || x == 4
            || y == 4 || y == 3
            || (z == 4 && y != 0)
            || (z == 0 && y != 0)
            || (x == 2) && (y == 1)
            || (z == 2) && (y == 0))
	        return true;
	    return false;
	}

	function createCube(i, j, k) {
	    var geo = new THREE.CubeGeometry(size, size, size);
	    geo.faceVertexUvs[0][0] = [bricks[j][4 - k][0], bricks[j][4 - k][1], bricks[j][4 - k][3]];
	    geo.faceVertexUvs[0][1] = [bricks[j][4 - k][1], bricks[j][4 - k][2], bricks[j][4 - k][3]];
	    geo.faceVertexUvs[0][2] = [clouds[j][k][0], clouds[j][k][1], clouds[j][k][3]];
	    geo.faceVertexUvs[0][3] = [clouds[j][k][1], clouds[j][k][2], clouds[j][k][3]];
	    geo.faceVertexUvs[0][4] = [crate[4 - k][i][0], crate[4 - k][i][1], crate[4 - k][i][3]];
	    geo.faceVertexUvs[0][5] = [crate[4 - k][i][1], crate[4 - k][i][2], crate[4 - k][i][3]];
	    /*geo.faceVertexUvs[0][6] = [stone[arrayi][arrayj][0], stone[arrayi][arrayj][1], stone[arrayi][arrayj][3]];
	    geo.faceVertexUvs[0][7] = [stone[arrayi][arrayj][1], stone[arrayi][arrayj][2], stone[arrayi][arrayj][3]];
	   */ geo.faceVertexUvs[0][8] = [water[j][i][0], water[j][i][1], water[j][i][3]];
	    geo.faceVertexUvs[0][9] = [water[j][i][1], water[j][i][2], water[j][i][3]];
	    geo.faceVertexUvs[0][10] = [wood[j][4 - i][0], wood[j][4 - i][1], wood[j][4 - i][3]];
	    geo.faceVertexUvs[0][11] = [wood[j][4 - i][1], wood[j][4 - i][2], wood[j][4 - i][3]];
	    var cube = createMesh(geo, "texture-atlas.jpg");

	    cube.position.x = i * size - n * size / 2 + size / 2;
	    cube.position.y = j * size - n * size / 2 + size / 2;
	    cube.position.z = k * size - n * size / 2 + size / 2;

	    return cube
	}

	function render() {
	    stats.update();
	    checkSpherePosition();
	    //trackBallControls.update();

	    megaCube.rotation.y = step += 0.01;
	    stone.rotation.y = step;
	   //megaCube.rotation.x = step;
       //
	   //megaCube.rotation.y = Math.PI / 2 * 3;
	   //megaCube.rotation.y = Math.PI / 2 * 3;

		// render using requestAnimationFrame
		requestAnimationFrame(render);
		webGLRenderer.render(scene, camera);
	}

	function checkSpherePosition() {
	    if (currentView == 0) {
	        moveTo(megaCube.position);
	    }
	    else
	        moveTo(stone.position);
	}

	function moveTo(position) {
	    if (sphere.position.x > position.x)
	    {
	        sphere.position.x = sphere.position.x - distance / 100 * params.cameraSpeed;
	        
	        if (sphere.position.x < position.x)
	            sphere.position.x = position.x;
	    }
	    else if (sphere.position.x < position.x) {
	        sphere.position.x = sphere.position.x + distance / 100 * params.cameraSpeed;
	        if (sphere.position.x > position.x)
	            sphere.position.x = position.x;
	    }
	    var sign;
	    if (sphere.position.x <= distance / 2)
	        sign = 1;
	    else sign = -1;
	    cameraUpPosition = Math.abs(sphere.position.x - distance / 2) / (distance / 2);

	    camera.up.set(0, 1, 1 - cameraUpPosition);
	    camera.lookAt(sphere.position);
	}

    // setup the control gui
	var controls = new function () {

	};

	function createMesh(geom, imageFile) {
	    var texture = THREE.ImageUtils.loadTexture(imageFile)

	    var mat = new THREE.MeshPhongMaterial();
	    mat.map = texture;

	    var mesh = new THREE.Mesh(geom, mat);
	    return mesh;
	}

	function createImgVectors() {
	    bricks  = createImgVector(0.5,  0.333,  0,    0.666,  0.5,    1);
	    clouds  = createImgVector(0.5,  0.333,  0.5,  0.666,  1,      1);
	    crate   = createImgVector(0.5,  0.333,  0,    0.333,  0.5,    0.666);
	    stone   = createImgVector(0.5,  0.333,  0.5,  0.333,  1,      0.666);
        water   = createImgVector(0.5,  0.333,  0,    0,      0.5,    0.333);
        wood    = createImgVector(0.5,  0.333,  0.5,  0,      1,      0.333);

	    /*bricks = createImgVector(0.5, 0.333, 0.5, 0.666, 1, 1);
	    clouds = createImgVector(0.5, 0.333, 0.5, 0.666, 1, 1);
	    crate = createImgVector(0.5, 0.333, 0.5, 0.666, 1, 1);
	    stone = createImgVector(0.5, 0.333, 0.5, 0.666, 1, 1);
	    water = createImgVector(0.5, 0.333, 0.5, 0.666, 1, 1);
	    wood = createImgVector(0.5, 0.333, 0.5, 0.666, 1, 1);*/
	}

	function createImgVector(x, y, x1, y1, x2, y2) {
	    /*
             var bricks = [new THREE.Vector2(0, .666), new THREE.Vector2(.5, .666), new THREE.Vector2(.5, 1), new THREE.Vector2(0, 1)];
	         var clouds = [new THREE.Vector2(.5, .666), new THREE.Vector2(1, .666), new THREE.Vector2(1, 1), new THREE.Vector2(.5, 1)];
	         var crate = [new THREE.Vector2(0, .333), new THREE.Vector2(.5, .333), new THREE.Vector2(.5, .666), new THREE.Vector2(0, .666)];
	         var stone = [new THREE.Vector2(.5, .333), new THREE.Vector2(1, .333), new THREE.Vector2(1, .666), new THREE.Vector2(.5, .666)];
	         var water = [new THREE.Vector2(0, 0), new THREE.Vector2(.5, 0), new THREE.Vector2(.5, .333), new THREE.Vector2(0, .333)];
	         var wood = [new THREE.Vector2(.5, 0), new THREE.Vector2(1, 0), new THREE.Vector2(1, .333), new THREE.Vector2(.5, .333)];
		*/
	    var vector2d = [[], [], [], [], [] ,[]];
	    for (i = 0; i < 5; i++)
	        for (j = 0; j < 5; j++)
	        {
	            var cx1 = x1 + x / n * i;
	            var cx2 = x2 - x / n * (n - i - 1);
	            var cy1 = y1 + y / n * j;
	            var cy2 = y2 - y / n * (n - j - 1);
	            var vector = [new THREE.Vector2(cx1, cy1), new THREE.Vector2(cx2, cy1),
                    new THREE.Vector2(cx2, cy2), new THREE.Vector2(cx1, cy2)];
	            vector2d[4 - i][j] = vector;
	        }
	    return vector2d;
	}

	function initStats() {

	    var stats = new Stats();
	    stats.setMode(0); // 0: fps, 1: ms

	    // Align top-left
	    stats.domElement.style.position = 'absolute';
	    stats.domElement.style.left = '0px';
	    stats.domElement.style.top = '0px';

	    $("#Stats-output").append(stats.domElement);

	    return stats;
	}
});