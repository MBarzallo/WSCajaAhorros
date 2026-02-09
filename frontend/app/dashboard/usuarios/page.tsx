"use client";

import { usuariosService } from "@/app/services/user.service";
import { useEffect, useState } from "react";

interface Usuario {
  id: string;
  nombreUsuario: string;
  correoElectronico: string;
  estaActivo: boolean;
  fechaCreacion: string;
}

export default function UsuariosPage() {
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const [loading, setLoading] = useState(true);

  // modal crear
  const [open, setOpen] = useState(false);
  const [nombreUsuario, setNombreUsuario] = useState("");
  const [correo, setCorreo] = useState("");
  const [saving, setSaving] = useState(false);

  const cargarUsuarios = async () => {
    setLoading(true);
    try {
      const res: any = await usuariosService.getAll();
      setUsuarios(res.data);
    } catch (e) {
      alert("Error al cargar usuarios");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    cargarUsuarios();
  }, []);

  const crearUsuario = async () => {
    if (!nombreUsuario || !correo) {
      alert("Completa todos los campos");
      return;
    }

    setSaving(true);
    try {
      await usuariosService.create({
        nombreUsuario,
        correoElectronico: correo,
      });
      setOpen(false);
      setNombreUsuario("");
      setCorreo("");
      cargarUsuarios();
    } catch (e: any) {
      alert(e.message ?? "Error al crear usuario");
    } finally {
      setSaving(false);
    }
  };

  const activar = async (id: string) => {
    await usuariosService.activar(id);
    cargarUsuarios();
  };

  const desactivar = async (id: string) => {
    await usuariosService.desactivar(id);
    cargarUsuarios();
  };

  return (
    <div className="p-8">
      {/* Header */}
      <div className="flex justify-between items-center mb-6">
        <div>
          <h1 className="text-2xl font-semibold text-gray-800">
            Usuarios del sistema
          </h1>
          <p className="text-sm text-gray-500">
            Gestión de usuarios internos de la caja de ahorros
          </p>
        </div>

        <button
          onClick={() => setOpen(true)}
          className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700"
        >
          + Nuevo usuario
        </button>
      </div>

      {/* Tabla */}
      <div className="bg-white rounded-xl shadow overflow-hidden">
        {loading ? (
          <div className="p-6 text-center text-gray-500">Cargando...</div>
        ) : (
          <table className="w-full text-sm">
            <thead className="bg-gray-100 text-gray-600">
              <tr>
                <th className="p-4 text-left">Usuario</th>
                <th className="p-4 text-left">Correo</th>
                <th className="p-4 text-left">Estado</th>
                <th className="p-4 text-left">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {usuarios.map((u) => (
                <tr key={u.id} className="border-t">
                  <td className="p-4 font-medium">
                    {u.nombreUsuario}
                  </td>
                  <td className="p-4">{u.correoElectronico}</td>
                  <td className="p-4">
                    <span
                      className={`px-3 py-1 rounded-full text-xs font-medium
                        ${
                          u.estaActivo
                            ? "bg-green-100 text-green-700"
                            : "bg-red-100 text-red-700"
                        }`}
                    >
                      {u.estaActivo ? "Activo" : "Inactivo"}
                    </span>
                  </td>
                  <td className="p-4">
                    {u.estaActivo ? (
                      <button
                        onClick={() => desactivar(u.id)}
                        className="text-red-600 hover:underline"
                      >
                        Desactivar
                      </button>
                    ) : (
                      <button
                        onClick={() => activar(u.id)}
                        className="text-green-600 hover:underline"
                      >
                        Activar
                      </button>
                    )}
                  </td>
                </tr>
              ))}

              {usuarios.length === 0 && (
                <tr>
                  <td
                    colSpan={4}
                    className="p-6 text-center text-gray-500"
                  >
                    No existen usuarios
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        )}
      </div>

      {/* MODAL CREAR */}
      {open && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
          <div className="bg-white w-full max-w-md rounded-xl p-6">
            <h2 className="text-lg font-semibold mb-4">
              Crear nuevo usuario
            </h2>

            <div className="space-y-4">
              <div>
                <label className="text-sm text-gray-600">
                  Nombre de usuario
                </label>
                <input
                  value={nombreUsuario}
                  onChange={(e) => setNombreUsuario(e.target.value)}
                  className="w-full border rounded-lg px-3 py-2 mt-1"
                />
              </div>

              <div>
                <label className="text-sm text-gray-600">
                  Correo electrónico
                </label>
                <input
                  value={correo}
                  onChange={(e) => setCorreo(e.target.value)}
                  type="email"
                  className="w-full border rounded-lg px-3 py-2 mt-1"
                />
              </div>
            </div>

            <div className="flex justify-end gap-3 mt-6">
              <button
                onClick={() => setOpen(false)}
                className="px-4 py-2 text-gray-600 hover:underline"
              >
                Cancelar
              </button>
              <button
                disabled={saving}
                onClick={crearUsuario}
                className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 disabled:opacity-50"
              >
                {saving ? "Guardando..." : "Crear"}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
