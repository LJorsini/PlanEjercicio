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
                          <button type="button" class="btn btn-success" onclick="AbrirModalEdita(${tipoDeEjercicio.idEjercicio})">
                            Editar
                          </button>
                        </td>

                        <td>
                          <button type="button" class="btn btn-danger" onclick="ValidacionEliminar(${tipoDeEjercicio.idEjercicio})">
                            Eliminar
                          </button>
                        </td>

                    </tr>
                `;
          });
    
          document.getElementById("tbody-ejercicioFisico").innerHTML = contenidoTabla;
        },
    
        error: function (xhr, status) {
          console.log("Existio un problema al cargar el listado 123");
        },
      });
    }
