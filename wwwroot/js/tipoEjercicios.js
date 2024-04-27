window.onload = ListaEjercicio();

function ListaEjercicio() {
  $.ajax({
    //URL de la petision
    url: "../../TipoEjercicios/ListaEjercicio",
    //Informacion a eviar
    data: {},
    //Tipo de petision
    type: "POST",
    //Tipo de informacion en la respuesta
    dataType: "json",
    //Si la petision es satisfactoria se ejecuta este codigo
    //la respuesta es pasada como argumento a la funcion
    success: function (tipoDeEjercicios) {
      let contenidoTabla = "";

      $.each(tipoDeEjercicios, function (index, tipoDeEjercicio) {
        contenidoTabla += `
                    <tr>
                        <td>${tipoDeEjercicio.nombreEjercicio}</td>
                        <td class="text-center">
                        <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${tipoDeEjercicio.idEjercicio})">
                        Editar
                        </button>
                        </td>

                        <td class="text-center">
                        <button type="button" class="btn btn-danger" onclick="Validacion(${tipoDeEjercicio.idEjercicio})">
                        Eliminar
                        </button>
                        </td>
                    <tr>
                `;
      });
      document.getElementById("tbody-tipoEjercicio").innerHTML = contenidoTabla;
    },
    error: function (xhr, status) {
      console.log("Disculpe, existio un problema al cargar el listado");
    },
  });
}

function LimpiarModal() {
    document.getElementById("TipoEjercicioId").value = 0;
    document.getElementById("descripcion").value = "";
}

function NuevoRegistro() {
    $("#ModalTitulo").text("Nuevo tipo de ejercicio");
    
}


//Modal editar
function AbrirModalEditar(tipoEjercicioId){
    $.ajax({
        url: "../../TipoEjercicios/ListaEjercicio",
        data: { data: tipoEjercicioId },
        type: 'POST',
        dataType: 'json',
        success: function (tipoDeEjercicios) {
            let tipoDeEjercicio = tipoDeEjercicios[0];

            document.getElementById("TipoEjercicioId").value = tipoEjercicioId;
            $("#ModalTitulo").text("Editar tipo de ejercicio");
            document.getElementById("descripcion").value = tipoDeEjercicio.nombreEjercicio;
            $("#ModalTipoEjercicio").modal("show");
        },
        error: function (xhr, status) {
            console.log("Hubo un problema al consultar el registro");
        }
    })
}


//Funcion para guarar un registro
function GuardarEjercicio() {
    let tipoEjercicioId = document.getElementById("TipoEjercicioId").value;
    let descripcionEjercicio = document.getElementById("descripcion").value;

    console.log(descripcionEjercicio);

    $.ajax({
        url: "../../TipoEjercicios/GuardarTipoEjercicio",
        data: {IdEjercicio: tipoEjercicioId, NombreEjercicio: descripcionEjercicio },
        type: 'POST',
        dataType: 'json',

        success: function (resultado) {
            
            if(resultado != "") {
                alert(resultado);
            }
            ListaEjercicio();
        },
        error: function (xhr, status) {
            console.log("Hubo un problema al cargar el registro");
        }
    });
}


//validacion eliminar
function Validacion(tipoEjercicioId) {
    var deseaEliminar = alert("Desea eliminar el registro?")
    
    if (deseaEliminar == true) {
        EliminarEjercicio(tipoEjercicioId);
    }
}

function EliminarEjercicio (tipoEjercicioId){
    $.ajax({
        url: "../../Tipoejercicios/EliminarTipoEjercicio",
        data: {IdEjercicio: tipoEjercicioId },
        type: 'POST',
        dataType: 'json',

        success: function (resultado) {
            
            ListaEjercicio();
        },
        error: function (xhr, status) {
            console.log("Hubo un problema al cargar el registro");
        }

    })

}






