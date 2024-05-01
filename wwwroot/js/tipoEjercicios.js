window.onload = ListaEjercicio();

//Funcion que gace que se actualice la lista de ejercicios
function ListaEjercicio() {
  $.ajax({
    //url desde donde se va a hacer la petision
    url: "../../TipoEjercicios/ListadoEjercicios",
    //Datos que se van a enviar
    data: {},
    //Tipo de petision
    type: "POST",
    //tipo de informacion que se devuelve
    dataType: "json",
    //si la respuesta es correcta, se ejecuta el siguente codigo
    //la respuesta es pasada por parametro como argumento de la funcion
    success: function (tipoEjercicios) {
      $('#ModalTipoEjercicio').modal("hide");
      LimpiarModal();

      let contenidoTabla = ``;

      $.each(tipoEjercicios, function (index, tipoDeEjercicio) {
        contenidoTabla += `
                <tr>
                    <td>${tipoDeEjercicio.nombreEjercicio}</th>
                    <td class="text.center">
                    <button type="button" class="btn btn-success" onclick="AbrirModalEdita(${tipoDeEjercicio.idEjercicio})">
                     Editar
                    </button>

                    <td class="text.center">
                    <button type="button" class="btn btn-danger" onclick="ValidacionEliminar(${tipoDeEjercicio.idEjercicio})">
                     Eliminar
                    </button>
                </tr>
            `;
      });

      document.getElementById("tbody-tipoEjercicio").innerHTML = contenidoTabla;
    },

    error: function (xhr, status) {
      console.log("Existio un problema al cargar el listado");
    },
  });
}
//Funcion para cargar nuevo ejercicio
function GuardarEjercicio() {
    let tipoEjercicioID = document.getElementById("TipoEjercicioId").value;
    let descripcion = document.getElementById("descripcion").value;

    $.ajax({
        url: '../../TipoEjercicios/CargarNuevoEjercicio',
        data: {tipoEjercicioID: tipoEjercicioID, descripcion: descripcion},
        type: 'POST',
        dataType: 'json',
        
        success: function(resultado) {
            if(resultado != ""){
                alert(resultado)
            }
            ListaEjercicio();
        },

        error: function (xhr, status) {
            console.log("Existion un problema al cargar el registro")
        }
    });
}
//Funcion que le pregunta al usuario si quiere eliminar un registro

function ValidacionEliminar(idEjercicio) {
    console.log("Boton funciona");
  var deseaEliminar = confirm("¿Desea Eliminar la actividad?");

  if (deseaEliminar == true) {
    EliminarActividad(idEjercicio);
  }
}


//Funcion eliminar registro
function EliminarActividad(idEjercicio) {
  $.ajax({
    url: "../../TipoEjercicios/EliminarRegistro",
    data: { idEjercicio: idEjercicio },
    type: "POST",
    dataType: "Json",

    success: function (resultado) {
      ListaEjercicio();
    },
    error: function (xhr, status) {
      alert("hubo un error");
    },
  });
}

//Modal Editar 
function AbrirModalEdita(idEjercicio) {
  console.log("Boton funciona")
  $.ajax({
    url: "../../TipoEjercicios/ListadoEjercicios",
    data: { idEjercicio : idEjercicio},
    type: "POST",
    dataType: "json",
    
    success: function(tipoEjercicios){
      let tipoDeEjercicio = tipoEjercicios[0];

      document.getElementById("TipoEjercicioId").value = idEjercicio;
      $("#ModalTitulo").text("Editar tipo de ejercicio");
      var prueba = document.getElementById("descripcion").value = tipoDeEjercicio.nombreEjercicio;
      console.log(prueba);
      $("#ModalTipoEjercicio").modal("show");

    },

    error: function (xhr, status) {
      console.log("No se puede editar el registro")
    }
  })


}


function LimpiarModal ()
{
    document.getElementById("TipoEjercicioId").value = 0;
    document.getElementById("descripcion").value = "";
}
