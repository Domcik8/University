// once everything is loaded, we run our Three.js stuff.
$(function () {

	// create a scene, that will hold all our elements such as objects, cameras and lights.
	var scene = new THREE.Scene();

	// create a camera, which defines where we're looking at.
	var camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 0.1, 1000);

	// create a render and set the size
	var renderer = new THREE.WebGLRenderer();

	renderer.setClearColorHex(0xEEEEEE, 1.0);
	renderer.setSize(window.innerWidth, window.innerHeight);
	renderer.shadowMapEnabled = true;
	
	// position and point the camera to the center of the scene
	camera.position.x = -50;
	camera.position.y = 40;
	camera.position.z = -45;
	camera.lookAt(scene.position);

	// add spotlight for the shadows
	var spotLight = new THREE.SpotLight(0xFFFFFF);
	spotLight.position.set(-40, 60, -10);
	spotLight.castShadow = true;
	scene.add(spotLight);
	
	var ambientLight = new THREE.AmbientLight(0x404040); // soft white light
	scene.add(ambientLight);

	// axes helper
	/*var axes = new THREE.AxisHelper(20);
	scene.add(axes);*/

	addPlane(15, 0, 0, "floor1", 250, 250);
	
	var params = new function () {
	    this.floor2Height = 15;
	    this.stepLenght = 10;
	    this.stepWidth = 0;
	    this.stepGap = 5;
	    this.rotation = 90;
	    this.stepQuantity = 15;
	    this.radius = 0;
	    this.railingHeight = 7;
	    this.floor2x = 0;
	    this.floor2z = 0;
		
		this.update = function () {
		    var selectedObject = scene.getObjectByName("floor2");
		    scene.remove(selectedObject);
		    var selectedObject = scene.getObjectByName("railing");
		    scene.remove(selectedObject);
		    clearStairs();
		    addPlane(15 + params.floor2x, params.floor2Height, 27 + params.floor2z, "floor2", 80, 50);
            
		    /*var shape = createExtrudeStep(params);
		    
		    shape.position.z -= 30;

            scene.add(shape);*/


			addSteps(params);
			addRailing(params);
		};
	}

	var gui = new dat.GUI();
	gui.add(params, 'floor2Height', 0, 100).onChange(params.update);
	gui.add(params, 'stepLenght', 0, 25).onChange(params.update);
	gui.add(params, 'stepWidth', 0, 20).onChange(params.update);
	gui.add(params, 'stepGap', 1, 10).onChange(params.update);
	gui.add(params, 'rotation', -360, 360).onChange(params.update);
	//gui.add(params, 'radius', 0, 100).onChange(params.update);
	gui.add(params, 'stepQuantity', 0, 25).onChange(params.update);
	gui.add(params, 'railingHeight', 0, 10).onChange(params.update);
	gui.add(params, 'floor2x', 0, 30).onChange(params.update);
	gui.add(params, 'floor2z', 0, 30).onChange(params.update);

	params.update();

	// add the output of the renderer to the html element
	$("#WebGL-output").append(renderer.domElement);
	var controls = new THREE.TrackballControls(camera, renderer.domElement);     
	render();

	function render() {
		controls.update();
		requestAnimationFrame(render);
		renderer.render(scene, camera);
	}
	
	function clearStairs() {
		for (i = 0; i < 25; i++) {
			scene.remove(scene.getObjectByName("step" + i));
		}
	}
	
	function addPlane(x, y, z, name, width, length) {
		// create the ground plane
	    var planeGeometry = new THREE.PlaneGeometry(width, length);
		var planeMaterial = new THREE.MeshLambertMaterial({color: 0xffffff});
		var plane = new THREE.Mesh(planeGeometry, planeMaterial);
		plane.receiveShadow  = true;
		plane.name = name;

		// rotate and position the plane
		plane.rotation.x = -0.5 * Math.PI;
		plane.position.x = x - 10;
		plane.position.y = y;
		plane.position.z = z;

		// add the plane to the scene
		scene.add(plane);
	}
	
	function addShape(stepLength, stepHeight, stepGap) {
	    var stepOffset = stepHeight / stepGap * (stepGap - 1);
	    var shape = new THREE.Shape();

	    shape.moveTo(stepOffset, stepOffset);
	    shape.lineTo(stepOffset, stepHeight);
	    shape.lineTo(stepLength, stepHeight);
	    shape.lineTo(stepLength, stepOffset);

	    /*shape.moveTo(stepOffset, stepOffset);
	    shape.lineTo(stepLength, stepOffset);
	    shape.lineTo(stepLength, stepLength / 5 * 3);
	    shape.lineTo(stepLength / 5 * 3, stepLength);
	    shape.lineTo(stepOffset, stepLength);*/

	    /*shape.moveTo(0, 0);
	    shape.lineTo(25, 0);
	    shape.lineTo(25, 15);
	    shape.lineTo(15, 25);
	    shape.lineTo(0, 25);*/

		return shape;
	}

	function addSteps(params) {
	    var step, box, i;
	    var stepRotation = params.rotation / (params.stepQuantity - 1) * Math.PI / 180;
	    var stepHeight = params.floor2Height / params.stepQuantity;

	    /*var stepWidth = 5;
	    var cubeGeometry = new THREE.CubeGeometry(radius, stepThickness, stepWidth);
	    var cubeMaterial = new THREE.MeshLambertMaterial({ color: 0xff0000 });*/

	    for (i = 0; i < params.stepQuantity; i++) {

	        box = createExtrudeStep(params, i);
	        box.castShadow = true;

	      /*  if (i % 2 == 0)
	        {
	            box.rotation.z = Math.PI / 2;
	            box.position.x = params.stepLenght;
	        }*/

	        // position the step
	        box.position.x += params.radius + params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i));
	        box.position.y = stepHeight * (params.stepQuantity - i - 1);
	        box.position.z = params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i));
	        // rotate
	        step = new THREE.Object3D();
	        step.add(box);
	        if (params.stepQuantity != 1)
	        {
	            step.rotation.y = stepRotation * i;
                //step
	        }
	        step.name = "step" + i;

	        // add the step to the scene
	        //step.castShadow = true;
	        scene.add(step);
	    }
	}

	function addRailing(params) {
	    var stepRotation = params.rotation / (params.stepQuantity - 1) * Math.PI / 180;
	    var stepHeight = params.floor2Height / params.stepQuantity;

	    var points = [];
	    for (i = 0; i < params.stepQuantity + 1; i = i + 1) {
	        var x = (-params.stepLenght - params.radius - params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i))) * Math.sin(stepRotation * i - Math.PI / 2) + params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i)) * Math.cos(stepRotation * i - Math.PI / 2);
	        var y = stepHeight * (params.stepQuantity - i) + params.railingHeight;
	        var z = (-params.stepLenght - params.radius - params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i))) * Math.cos(stepRotation * i - Math.PI / 2) - params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i)) * Math.sin(stepRotation * i - Math.PI / 2);

	        points.push(new THREE.Vector3(x, y, z));
	    }
	    var curve = new THREE.SplineCurve3(points);
	    var geometry = new THREE.TubeGeometry(curve, 1000, 0.5, 8, false);
	    var material = new THREE.MeshPhongMaterial({ color: 0xA52A2A });
	    var splineObject = new THREE.Mesh(geometry, material);

	    var railing = new THREE.Object3D();
	    railing.add(splineObject);
	    railing.name = "railing";

	    for (i = 0; i < points.length; i++) {
	        var curve = new THREE.SplineCurve3([points[i], new THREE.Vector3(points[i].x, points[i].y - params.railingHeight, points[i].z)]);
	        var geometry = new THREE.TubeGeometry(curve, 100, 0.25, 8, true);
	        var splineObject = new THREE.Mesh(geometry, material);
	        railing.add(splineObject);
	    }
        
	   for (i = 0; i < points.length - 1; i++) {
	       var curve = new THREE.SplineCurve3([new THREE.Vector3(
               (-params.stepLenght / 2 - params.radius - params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i))) * Math.sin(stepRotation * i - Math.PI / 2) + params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i)) * Math.cos(stepRotation * i - Math.PI / 2),
               points[i].y - 0.1 - params.railingHeight,
               (-params.stepLenght / 2 - params.radius - params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i))) * Math.cos(stepRotation * i - Math.PI / 2) - params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i)) * Math.sin(stepRotation * i - Math.PI / 2)),

            new THREE.Vector3(
                (-params.stepLenght / 2 - params.radius - params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i))) * Math.sin(stepRotation * i - Math.PI / 2) + params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i)) * Math.cos(stepRotation * i - Math.PI / 2),
                points[i].y - stepHeight - params.railingHeight,
                (-params.stepLenght / 2 - params.radius - params.floor2x * Math.sin(stepRotation * (params.stepQuantity - i))) * Math.cos(stepRotation * i - Math.PI / 2) - params.floor2z * Math.sin(stepRotation * (params.stepQuantity - i)) * Math.sin(stepRotation * i - Math.PI / 2))]);

	        var geometry = new THREE.TubeGeometry(curve, 100, 0.25, 8, true);
	        var splineObject = new THREE.Mesh(geometry, material);
	        railing.add(splineObject);
	    }

	    scene.add(railing);
	}

	function createExtrudeStep(params) {

	    var options = {
	        amount: params.stepWidth,
	        bevelThickness: 2/*,
	        bevelSize: 0.5,
            bevelSegments: 15*/
	    };

	    var extrudeGeometry = new THREE.ExtrudeGeometry(addShape(params.stepLenght, params.floor2Height / params.stepQuantity, params.stepGap), options);

	    //extrudeGeometry.rotation.y = Math.PI;

	    extrudeGeometry.applyMatrix(new THREE.Matrix4().makeTranslation(0, 0, 0));
		// assign two materials
	    var meshLambertMaterial = new THREE.MeshLambertMaterial({color: 0xFFDEAD});

		// create a multimaterial
	    var mesh = THREE.SceneUtils.createMultiMaterialObject(extrudeGeometry, [meshLambertMaterial]);

	    /*var test = new THREE.Object3D();
	    test.position.x = params.stepWidth;
	    mesh.add(test);*/

	    mesh.children[0].castShadow = true;


	   // mesh.rotation.x = -Math.PI / 2;
		return mesh;
	}
});