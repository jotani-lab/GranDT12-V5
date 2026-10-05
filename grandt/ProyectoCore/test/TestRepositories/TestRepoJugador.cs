using ProyectoCore.Models;
using ProyectoCore.Repositories.RepoClase;
using Xunit;

namespace ProyectoCore.Test.TestRepositories;

public class TestRepoJugador
{
    [MySqlFact]
    public void CrearYConsultar_LeeJugadorYRelaciones()
    {
        using var db = new TestRepositorioSupport();
        var equipoId = db.CrearEquipo();
        var posicionId = db.CrearPosicion();
        var jugador = new Jugador
        {
            Nombre = "Jugador de prueba",
            Apellido = "Integracion",
            FechaNacimiento = new DateTime(1990, 1, 1),
            EquipoId = equipoId,
            PosicionId = posicionId,
            Cotizacion = 1234.56m
        };
        var repo = new RepoJugador(db.Db);

        var id = repo.Crear(jugador);
        db.RegistrarJugador(id);
        var obtenido = repo.ObtenerPorId(id);

        Assert.NotNull(obtenido);
        Assert.Equal(equipoId, obtenido.Equipo?.Id);
        Assert.Equal(posicionId, obtenido.Posicion?.Id);
        Assert.Contains(repo.ObtenerTodos(), j => j.Id == id);
    }
}
