window.onload = mostrarEjerciciosFisicos();

function mostrarEjerciciosFisicos() {
  console.log("Boton funciona");
  $.ajax({
    //url desde donde se va a hacer la petision
    url: "../../EjerciciosFisicos/ListadoEjerciciosFisicos",
    //Datos que se van a enviar
    data: {},
    //Tipo de petision
    type: "POST",
    //tipo de informacion que se devuelve
    dataType: "json",
    //si la respuesta es correcta, se ejecuta el siguente codigo
    //la respuesta es pasada por parametro como argumento de la funcion
    success: function (ejercicio) {
      /* $('#ModalTipoEjercicio').modal("hide"); */
      /* LimpiarModal(); */
      console.log(ejercicio);
      let contenidoTabla = ``;

      $.each(ejercicio, function (index, tipoDeEjercicio) {
        contenidoTabla += `
                    <tr>
                        <th scope="row">${tipoDeEjercicio.tipoEjercicioNombre}</th>
                        <td>${tipoDeEjercicio.inicioString}</td>
                        <td>${tipoDeEjercicio.finString}</td>
                        <td>@${tipoDeEjercicio.observaciones}</td>
                        <td>
                          <button type="button" class="btn btn-success" onclick="AbrirModalEdita(${tipoDeEjercicio.ejercicioFisicoId})">
                            Editar
                          </button>
                        </td>

                        <td>
                          <button type="button" class="btn btn-danger" onclick="ValidacionEliminar(${tipoDeEjercicio.ejercicioFisicoId})">
                            Eliminar
                          </button>
                        </td>

                    </tr>
                `;
      });

      document.getElementById("tbody-ejercicioFisico").innerHTML =
        contenidoTabla;
    },

    error: function (xhr, status) {
      console.log("Existio un problema al cargar el listado 123");
    },
  });
}

function CargarDatosEjercicio() {
            let ejercicioFisicoId = document.getElementById("EjercicioFisicoID").value;
            let  tipoEjercicioID= document.getElementById("TipoEjercicioID").value;
            let fechaInicio = document.getElementById("FechaInicio").value;
            let fechaFin = document.getElementById("FechaFin").value;
            let estadoEmocionalInicio = document.getElementById("EstadoEmocionalInicio").value;
            let estadoEmocionalFin = document.getElementById("EstadoEmocionalFin").value;
            let observaciones = document.getElementById("Observaciones").value;
  $.ajax({
    url: "../../EjerciciosFisicos/CargarEjercicios",
    data: {ejercicioFisicoId: ejercicioFisicoId, tipoEjercicioID: tipoEjercicioID, fechaInicio: fechaInicio, fechaFin: fechaFin, estadoEmocionalInicio:estadoEmocionalInicio, estadoEmocionalFin: estadoEmocionalFin, observaciones: observaciones,   
      
    },
    type: "POST",
    dataType: "json",

    success: function (resultado) {
      if (resultado != "") {
        alert(resultado);
      }
      mostrarEjerciciosFisicos();
    },

    error: function (xhr, status) {
      console.log("Existion un problema al cargar el registro");
    },
  });
}

//Modal Editar
function AbrirModalEdita(ejercicioFisicoId) {
  console.log("Boton funciona");
  $.ajax({
    url: "../../EjerciciosFisicos/ListadoEjercicios",
    data: { ejercicioFisicoId: ejercicioFisicoId },
    type: "POST",
    dataType: "json",

    success: function (tipoEjercicios) {
      let tipoDeEjercicio = tipoEjercicios[0];

            document.getElementById("EjercicioFisicoID").value = ejercicioFisicoId;
            $("#ModalTitulo").text("Editar Ejercicio");
            document.getElementById("TipoEjercicioID").value = tipoDeEjercicio.idEjercicio;
            document.getElementById("FechaInicio").value = tipoDeEjercicio.inicio;
            document.getElementById("FechaFin").value = tipoDeEjercicio.fin;
            document.getElementById("EstadoEmocionalInicio").value = tipoDeEjercicio.estadoEmocionalInicio;
            document.getElementById("EstadoEmocionalFin").value = tipoDeEjercicio.estadoEmocionalFin;
            document.getElementById("Observaciones").value = tipoDeEjercicio.observaciones;

            $("#ModalEjercicioFisico").modal("show");
    },

    error: function (xhr, status) {
      console.log("No se puede editar el registro");
    },
  });
}





//Limpiar modal
function LimpiarModal() {
            document.getElementById("EjercicioFisicoID").value = 0;
            
            document.getElementById("TipoEjercicioID").value = 0;
            document.getElementById("FechaInicio").value = "";
            document.getElementById("FechaFin").value = "";
            document.getElementById("EstadoEmocionalInicio").value = 0;
            document.getElementById("EstadoEmocionalFin").value = 0;
            document.getElementById("Observaciones").value = "";
}


function ValidacionEliminar(ejercicioFisicoId) {
  console.log("Boton funciona");

  /* var deseaEliminar = confirm("¿Desea Eliminar la actividad?"); */
  Swal.fire({
    title: "¿Desea eliminar?",

    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Eliminado",
  }).then((result) => {
    if (result.isConfirmed) {
      EliminarActividad(ejercicioFisicoId);
      Swal.fire({
        title: "Deleted!",
        text: "Your file has been deleted.",
        icon: "success",
      });
    }
  });

  /* if (deseaEliminar == true) {
    EliminarActividad(idEjercicio);
  }  */
}

//Funcion eliminar registro
function EliminarActividad(ejercicioFisicoId) {
  $.ajax({
    url: "../../TipoEjercicios/EliminarRegistro",
    data: { ejercicioFisicoId: ejercicioFisicoId },
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
//Eliminar
//Funcion eliminar registro
/* function EliminarActividad(ejercicioFisicoId) {
  $.ajax({
    url: "../../EjerciciosFisicos/EliminarRegistroEjercicio",
    data: { ejercicioFisicoId: ejercicioFisicoId },
    type: "POST",
    dataType: "Json",

    success: function (resultado) {
      ListaEjercicio();
    },
    error: function (xhr, status) {
      alert("hubo un error");
    },
  });
} */
