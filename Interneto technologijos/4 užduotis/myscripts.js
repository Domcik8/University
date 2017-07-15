function createNewDocument(){
	var date;
	if(!validateFullName())
	{
		return;
	}
	
	if(!validateAge())
	{
		alert("Amžius turi būti sveikas skaičius!");
		return;
	}
	
	date = validateDate()
	if(!date)
	{
		alert("Blogai įvesta data!");
		return;
	}
	
	if(!validateNumber()) {
		alert("Blogai įvestas tel. numeris!");
		return;
	}
	
	var bodyTag = document.getElementsByTagName("body")[0];
	var ul = document.createElement("ul");
	var li = document.createElement("li");
	var a = document.createElement("a");
	var em = document.createElement("em");
	em.appendChild(document.createTextNode("Programų Sistemų"));
	a.appendChild(em);
	a.appendChild(document.createTextNode(" Bakalauro diplomas"));
	li.appendChild(document.createTextNode("Laipsnis: "));
	li.appendChild(a);
	ul.appendChild(li);
	
	li = document.createElement("li");
	a = document.createElement("a");
	a.appendChild(document.createTextNode(document.getElementById("fvp1").value));
	a.appendChild(document.createTextNode(" "));
	a.appendChild(document.createTextNode(document.getElementById("fvp2").value));
	li.appendChild(document.createTextNode("Vardas, Pavardė: "));
	li.appendChild(a);
	ul.appendChild(li);
	
	li = document.createElement("li");
	a = document.createElement("a");
	a.appendChild(document.createTextNode(document.getElementById("fa").value));
	li.appendChild(document.createTextNode("Amžius: "));
	li.appendChild(a);
	ul.appendChild(li);
	
	li = document.createElement("li");
	a = document.createElement("a");
	a.appendChild(document.createTextNode(date.getYear()));
	a.appendChild(document.createTextNode("-"));
	a.appendChild(document.createTextNode(date.getMonth() + 1));
	a.appendChild(document.createTextNode("-"));
	a.appendChild(document.createTextNode(date.getDate()));
	li.appendChild(document.createTextNode("Mokslų pabaigos Data: "));
	li.appendChild(a);
	ul.appendChild(li);
	
	bodyTag.insertBefore(ul, document.getElementsByTagName("h1")[1]);
	//bodyTag.appendChild(ul);
	
}

function validateFullName(){
	var vp1 = document.getElementById("fvp1");
	var vp2 = document.getElementById("fvp2");
	if(vp1.value == "" || vp2.value == "")
	{
		alert("Laukai Vardas ir Pavardė yra privalomi!");
		return false;
	}
	if(!/^[A-Za-z]+\s?\w*$/.test(vp1.value))
	{
		alert("Neteisingas vardas");
		return false;
	}
	if(!/^[A-Za-z]+$/.test(vp2.value))
	{
		alert("Neteisinga pavardė");
		return false;
	}
	
	if(vp1.value.split(/\s/).length > 1){
		var vardas = vp1.value;
		var regex1 = /^([A-Za-z]+)/g;
		var regex2 = /(\w+)$/g;
		var myArray = /^([A-Za-z]+)\s?(\w+)*$/.exec(vp1.value);
		vardas = myArray[1].replace(regex1, "$1as");
		vardas += myArray[2].replace(regex2, " $1ius");
		vp1.value = vardas;
	}
	return true;
}

function validateAge(){
	var age = document.getElementById("fa");
	if(age.value < 0)
		return false;
	return true;
}

function validateDate(){
	var fday = document.getElementById("fd");
	var fmonth = document.getElementById("fm");
	var fyear = document.getElementById("fy");
	
	if(fday.value != parseInt(fday.value))
		return false;
	if(fmonth.value != parseInt(fmonth.value))
		return false;
	if(fyear.value != parseInt(fyear.value))
		return false;
	
	var day = parseInt(fday.value)
	var month = parseInt(fmonth.value) - 1
	var year = parseInt(fyear.value)
	
	if(month < 0 || month > 11)
	{
		return false;
	}
	
	var date = new Date(year, month, day);
	if (date.getDate() != day) 
	{
		return false;
	}
	
	return date;
}

function validateNumber(){
	var nr = document.getElementById("fn");
	if(!/^(8[0-9]{3}\d{5})$/.test(nr.value))
	{
		return false;
	}
	return true;
}

function Hide(){
	$("#nr1").hide();
};

function illegal(){
	$("#vp").text("Paulius Leleika");
	$("#vp").css({color: "orange"});
	$("#a2").remove();
	$("#senasDiplomas" ).append( "<li>Dokumentą patvirtino Vilniaus Universiteto diplomų komisiją</li>" );
}