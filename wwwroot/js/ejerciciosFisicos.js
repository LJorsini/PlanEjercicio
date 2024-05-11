window.onload = mostrarEjerciciosFisicos();

function mostrarEjerciciosFisicos ()
{
    console.log("Boton funciona");
    $.ajax({
        //url desde donde se va a hacer la petision
        url: "../../EjerciciosFisicos/ListadoEjerciciosFisicos",
        //Datos que se van a enviar
        data: { },
        //Tipo de petision
        type: "POST",
        //tipo de informacion que se devuelve
        dataType: "json",
        //si la respuesta es correcta, se ejecuta el siguente codigo
        //la respuesta es pasada por parametro como argumento de la funcion
        success: function (ejercicio) {
          /* $('#ModalTipoEjercicio').modal("hide"); */
          /* LimpiarModal(); */
    
          let contenidoTabla = ``;
    
          $.each(ejercicio, function (index, tipoDeEjercicio) {
            contenidoTabla += `
                    <tr>
                        <td>${tipoDeEjercicio.TipoEjercicioNombre}</th>
                        
                        
                        
                        
                        /* <td class="text.center">
                        <button type="button" class="btn btn-success" onclick="AbrirModalEdita(${tipoDeEjercicio.idEjercicio})">
                         Editar1
                        </button>
    
                        <td class="text.center">
                        <button type="button" class="btn btn-danger" onclick="ValidacionEliminar(${tipoDeEjercicio.idEjercicio})">
                         Eliminar
                        </button> */
                    </tr>
                `;
          });
    
          document.getElementById("tbody-tipoEjercicio").innerHTML = contenidoTabla;
        },
    
        error: function (xhr, status) {
          console.log("Existio un problema al cargar el listado 123");
        },
      });
    }
