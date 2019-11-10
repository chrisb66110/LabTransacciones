function agregarProfe() {
    document.getElementById("seleccionaProfesor").style.display = "none";
    document.getElementById("Cedula").removeAttribute('name');
    document.getElementById("agregarProfesor").style.display = "block";
    document.getElementById("CedulaInput").setAttribute('name','Cedula');
}

function ponerDropProfe() {
    document.getElementById("seleccionaProfesor").style.display = "block";
    document.getElementById("CedulaInput").removeAttribute('name');
    document.getElementById("agregarProfesor").style.display = "none";
    document.getElementById("Cedula").setAttribute('name', 'Cedula');
}

function agregarExamen() {
    document.getElementById("seleccionaExamen").style.display = "none";
    document.getElementById("SiglaExam").removeAttribute('name');
    document.getElementById("agregarExamen").style.display = "block";
    document.getElementById("SiglaInput").setAttribute('name', 'SiglaExam');
}

function ponerDropExamen() {
    document.getElementById("seleccionaExamen").style.display = "block";
    document.getElementById("SiglaInput").removeAttribute('name');
    document.getElementById("agregarExamen").style.display = "none";
    document.getElementById("SiglaExam").setAttribute('name', 'SiglaExam');
}