"use client";

import { useEffect, useMemo, useState } from "react";
import { sociosService } from "@/app/services/socios.service";

type Telefono = { numero: string; etiqueta: string; esPrincipal: boolean };
type Correo = { email: string; etiqueta: string; esPrincipal: boolean };
type Direccion = {
  esPrincipal: boolean;
  callePrincipal: string;
  ciudad: string;
  provincia: string;
  pais: string;
  calleSecundaria: string;
  referencia: string;
  etiqueta: string;
};

const emptyCreate = {
  tipoPersonaId: 1,
  tipoIdentificacionId: 1,
  identificacionNumero: "",
  nombres: "",
  apellidos: "",
  fechaNacimiento: "",
  razonSocial: "",
  nombreComercial: "",
  fechaConstitucion: "",
  telefonos: [] as Telefono[],
  correos: [] as Correo[],
  direcciones: [] as Direccion[],
};

const emptyEdit = {
  id: "",
  tipoPersonaId: 1,
  nombres: "",
  apellidos: "",
  razonSocial: "",
  nombreComercial: "",
};

function onlyOnePrincipal<T extends { esPrincipal: boolean }>(arr: T[], idx: number) {
  return arr.map((x, i) => ({ ...x, esPrincipal: i === idx }));
}

export default function SociosPage() {
  const [socios, setSocios] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  const [filters, setFilters] = useState({ identificacion: "", nombres: "", activo: "" });

  const [open, setOpen] = useState(false);
  const [mode, setMode] = useState<"create" | "edit">("create");
  const [createForm, setCreateForm] = useState<any>(emptyCreate);
  const [editForm, setEditForm] = useState<any>(emptyEdit);

  const title = useMemo(() => (mode === "create" ? "Nuevo Socio" : "Editar Socio"), [mode]);

  const fetchSocios = async () => {
    setLoading(true);
    try {
      const res: any = await sociosService.getAll({
        identificacion: filters.identificacion || undefined,
        nombres: filters.nombres || undefined,
        activo: filters.activo === "" ? undefined : filters.activo === "true",
      });
      setSocios(res.data ?? res);
    } catch (e) {
      console.error(e);
      alert("Error al cargar socios");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchSocios();
  }, []);

  const openCreate = () => {
    setMode("create");
    setCreateForm(emptyCreate);
    setOpen(true);
  };

  const openEdit = (s: any) => {
    setMode("edit");
    setEditForm({
      id: s.id,
      tipoPersonaId: s.tipoPersona,
      nombres: s.nombres ?? "",
      apellidos: s.apellidos ?? "",
      razonSocial: s.razonSocial ?? "",
      nombreComercial: s.nombreComercial ?? "",
    });
    setOpen(true);
  };

  const toggleEstado = async (s: any) => {
    try {
      s.estaActivo ? await sociosService.deactivate(s.id) : await sociosService.activate(s.id);
      fetchSocios();
    } catch (e) {
      console.error(e);
      alert("No se pudo cambiar el estado");
    }
  };

  // --------- helpers para listas (create) ----------
  const addTelefono = () =>
    setCreateForm({
      ...createForm,
      telefonos: [...createForm.telefonos, { numero: "", etiqueta: "Móvil", esPrincipal: createForm.telefonos.length === 0 }],
    });

  const addCorreo = () =>
    setCreateForm({
      ...createForm,
      correos: [...createForm.correos, { email: "", etiqueta: "Personal", esPrincipal: createForm.correos.length === 0 }],
    });

  const addDireccion = () =>
    setCreateForm({
      ...createForm,
      direcciones: [
        ...createForm.direcciones,
        {
          esPrincipal: createForm.direcciones.length === 0,
          callePrincipal: "",
          ciudad: "",
          provincia: "",
          pais: "Ecuador",
          calleSecundaria: "",
          referencia: "",
          etiqueta: "Domicilio",
        },
      ],
    });

  const save = async () => {
    try {
      if (mode === "create") {
        // convertir fechas a DateOnly string o null
        const payload = {
          ...createForm,
          fechaNacimiento: createForm.fechaNacimiento ? createForm.fechaNacimiento : null,
          fechaConstitucion: createForm.fechaConstitucion ? createForm.fechaConstitucion : null,
        };
        await sociosService.create(payload);
        alert("Socio creado correctamente");
      } else {
        await sociosService.update(editForm.id, {
          nombres: editForm.nombres || null,
          apellidos: editForm.apellidos || null,
          razonSocial: editForm.razonSocial || null,
          nombreComercial: editForm.nombreComercial || null,
        });
        alert("Socio actualizado");
      }

      setOpen(false);
      fetchSocios();
    } catch (e) {
      console.error(e);
      alert("Error al guardar (revisa validaciones / campos obligatorios)");
    }
  };

  return (
    <main className="min-h-screen bg-slate-100 p-10">
      <div className="max-w-7xl mx-auto bg-white rounded-2xl shadow-sm border border-slate-200 p-8">
        {/* HEADER */}
        <div className="flex items-center justify-between gap-4 mb-6">
          <div>
            <h1 className="text-xl font-semibold text-slate-900">Gestión de Socios</h1>
            <p className="text-sm text-slate-500">Buscar, registrar y administrar socios del core financiero.</p>
          </div>

          <button
            onClick={openCreate}
            className="bg-slate-900 text-white px-4 py-2 rounded-lg hover:bg-slate-800 transition"
          >
            + Nuevo socio
          </button>
        </div>

        {/* FILTERS */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-3 mb-6">
          <input
            className="border rounded-lg px-3 py-2"
            placeholder="Identificación"
            value={filters.identificacion}
            onChange={(e) => setFilters({ ...filters, identificacion: e.target.value })}
          />
          <input
            className="border rounded-lg px-3 py-2"
            placeholder="Nombre / Razón social"
            value={filters.nombres}
            onChange={(e) => setFilters({ ...filters, nombres: e.target.value })}
          />
          <select
            className="border rounded-lg px-3 py-2"
            value={filters.activo}
            onChange={(e) => setFilters({ ...filters, activo: e.target.value })}
          >
            <option value="">Todos</option>
            <option value="true">Activos</option>
            <option value="false">Inactivos</option>
          </select>
          <button
            onClick={fetchSocios}
            className="bg-slate-800 text-white rounded-lg px-4 py-2 hover:bg-slate-700 transition"
          >
            {loading ? "Buscando..." : "Buscar"}
          </button>
        </div>

        {/* TABLE */}
        <div className="overflow-x-auto rounded-xl border border-slate-200 bg-white">
          <table className="w-full text-sm">
            <thead className="bg-slate-50 text-slate-600">
              <tr>
                <th className="p-3 text-left">Identificación</th>
                <th className="p-3 text-left">Nombre</th>
                <th className="p-3 text-left">Tipo</th>
                <th className="p-3 text-left">Estado</th>
                <th className="p-3 text-right">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {socios.map((s) => (
                <tr key={s.id} className="border-t border-slate-100">
                  <td className="p-3">{s.identificacion?.numero}</td>
                  <td className="p-3">
                    {s.tipoPersona === 1 ? `${s.nombres ?? ""} ${s.apellidos ?? ""}` : s.razonSocial ?? "-"}
                  </td>
                  <td className="p-3">{s.tipoPersona === 1 ? "Natural" : "Jurídica"}</td>
                  <td className="p-3">
                    <span
                      className={`inline-flex px-2 py-1 rounded-full text-xs font-medium ${
                        s.estaActivo ? "bg-green-100 text-green-700" : "bg-red-100 text-red-700"
                      }`}
                    >
                      {s.estaActivo ? "Activo" : "Inactivo"}
                    </span>
                  </td>
                  <td className="p-3">
                    <div className="flex justify-end gap-2">
                      <button
                        onClick={() => openEdit(s)}
                        className="px-3 py-1.5 rounded-lg border border-slate-200 hover:bg-slate-50"
                      >
                        Editar
                      </button>
                      <button
                        onClick={() => toggleEstado(s)}
                        className="px-3 py-1.5 rounded-lg border border-slate-200 hover:bg-slate-50"
                      >
                        {s.estaActivo ? "Desactivar" : "Activar"}
                      </button>
                    </div>
                  </td>
                </tr>
              ))}

              {!loading && socios.length === 0 && (
                <tr>
                  <td colSpan={5} className="p-6 text-center text-slate-500">
                    No se encontraron socios
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>

      {/* MODAL */}
      {open && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center p-4">
          <div className="w-full max-w-3xl bg-white rounded-2xl shadow-xl border border-slate-200">
            <div className="flex items-center justify-between p-5 border-b border-slate-100">
              <div>
                <h2 className="text-lg font-semibold text-slate-900">{title}</h2>
                <p className="text-sm text-slate-500">
                  {mode === "create" ? "Registro completo según el DTO del backend." : "Actualización de datos generales."}
                </p>
              </div>
              <button onClick={() => setOpen(false)} className="px-3 py-1.5 rounded-lg hover:bg-slate-100">
                ✕
              </button>
            </div>

            <div className="p-5 space-y-6">
              {mode === "create" ? (
                <>
                  {/* Datos base */}
                  <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                    <div>
                      <label className="text-sm font-medium">Tipo persona</label>
                      <select
                        className="mt-1 w-full border rounded-lg px-3 py-2"
                        value={createForm.tipoPersonaId}
                        onChange={(e) => setCreateForm({ ...createForm, tipoPersonaId: Number(e.target.value) })}
                      >
                        <option value={1}>Natural</option>
                        <option value={2}>Jurídica</option>
                      </select>
                    </div>

                    <div>
                      <label className="text-sm font-medium">Tipo identificación (id)</label>
                      <input
                        className="mt-1 w-full border rounded-lg px-3 py-2"
                        value={createForm.tipoIdentificacionId}
                        onChange={(e) => setCreateForm({ ...createForm, tipoIdentificacionId: Number(e.target.value) })}
                        placeholder="Ej: 1"
                        type="number"
                        min={1}
                      />
                    </div>

                    <div>
                      <label className="text-sm font-medium">Identificación</label>
                      <input
                        className="mt-1 w-full border rounded-lg px-3 py-2"
                        value={createForm.identificacionNumero}
                        onChange={(e) => setCreateForm({ ...createForm, identificacionNumero: e.target.value })}
                        placeholder="Cédula / RUC"
                      />
                    </div>
                  </div>

                  {/* Natural vs Jurídica */}
                  {createForm.tipoPersonaId === 1 ? (
                    <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                      <div>
                        <label className="text-sm font-medium">Nombres</label>
                        <input
                          className="mt-1 w-full border rounded-lg px-3 py-2"
                          value={createForm.nombres}
                          onChange={(e) => setCreateForm({ ...createForm, nombres: e.target.value })}
                        />
                      </div>
                      <div>
                        <label className="text-sm font-medium">Apellidos</label>
                        <input
                          className="mt-1 w-full border rounded-lg px-3 py-2"
                          value={createForm.apellidos}
                          onChange={(e) => setCreateForm({ ...createForm, apellidos: e.target.value })}
                        />
                      </div>
                      <div>
                        <label className="text-sm font-medium">Fecha nacimiento</label>
                        <input
                          className="mt-1 w-full border rounded-lg px-3 py-2"
                          type="date"
                          value={createForm.fechaNacimiento}
                          onChange={(e) => setCreateForm({ ...createForm, fechaNacimiento: e.target.value })}
                        />
                      </div>
                    </div>
                  ) : (
                    <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                      <div className="md:col-span-2">
                        <label className="text-sm font-medium">Razón social</label>
                        <input
                          className="mt-1 w-full border rounded-lg px-3 py-2"
                          value={createForm.razonSocial}
                          onChange={(e) => setCreateForm({ ...createForm, razonSocial: e.target.value })}
                        />
                      </div>
                      <div>
                        <label className="text-sm font-medium">Nombre comercial</label>
                        <input
                          className="mt-1 w-full border rounded-lg px-3 py-2"
                          value={createForm.nombreComercial}
                          onChange={(e) => setCreateForm({ ...createForm, nombreComercial: e.target.value })}
                        />
                      </div>
                      <div>
                        <label className="text-sm font-medium">Fecha constitución</label>
                        <input
                          className="mt-1 w-full border rounded-lg px-3 py-2"
                          type="date"
                          value={createForm.fechaConstitucion}
                          onChange={(e) => setCreateForm({ ...createForm, fechaConstitucion: e.target.value })}
                        />
                      </div>
                    </div>
                  )}

                  {/* Telefonos */}
                  <div className="rounded-xl border border-slate-200 p-4">
                    <div className="flex items-center justify-between mb-3">
                      <h3 className="font-semibold">Teléfonos</h3>
                      <button onClick={addTelefono} className="text-sm px-3 py-1.5 rounded-lg bg-slate-900 text-white">
                        + Agregar
                      </button>
                    </div>

                    <div className="space-y-3">
                      {createForm.telefonos.map((t: Telefono, idx: number) => (
                        <div key={idx} className="grid grid-cols-1 md:grid-cols-4 gap-3">
                          <input
                            className="border rounded-lg px-3 py-2"
                            placeholder="Número"
                            value={t.numero}
                            onChange={(e) => {
                              const next = [...createForm.telefonos];
                              next[idx] = { ...t, numero: e.target.value };
                              setCreateForm({ ...createForm, telefonos: next });
                            }}
                          />
                          <input
                            className="border rounded-lg px-3 py-2"
                            placeholder="Etiqueta (ej: Móvil)"
                            value={t.etiqueta}
                            onChange={(e) => {
                              const next = [...createForm.telefonos];
                              next[idx] = { ...t, etiqueta: e.target.value };
                              setCreateForm({ ...createForm, telefonos: next });
                            }}
                          />
                          <label className="flex items-center gap-2 text-sm">
                            <input
                              type="radio"
                              checked={t.esPrincipal}
                              onChange={() =>
                                setCreateForm({ ...createForm, telefonos: onlyOnePrincipal(createForm.telefonos, idx) })
                              }
                            />
                            Principal
                          </label>
                          <button
                            className="border rounded-lg px-3 py-2 hover:bg-slate-50"
                            onClick={() => {
                              const next = createForm.telefonos.filter((_: any, i: number) => i !== idx);
                              setCreateForm({ ...createForm, telefonos: next });
                            }}
                          >
                            Quitar
                          </button>
                        </div>
                      ))}
                      {createForm.telefonos.length === 0 && (
                        <p className="text-sm text-slate-500">Agrega al menos un teléfono (si tu validator lo exige).</p>
                      )}
                    </div>
                  </div>

                  {/* Correos */}
                  <div className="rounded-xl border border-slate-200 p-4">
                    <div className="flex items-center justify-between mb-3">
                      <h3 className="font-semibold">Correos</h3>
                      <button onClick={addCorreo} className="text-sm px-3 py-1.5 rounded-lg bg-slate-900 text-white">
                        + Agregar
                      </button>
                    </div>

                    <div className="space-y-3">
                      {createForm.correos.map((c: Correo, idx: number) => (
                        <div key={idx} className="grid grid-cols-1 md:grid-cols-4 gap-3">
                          <input
                            className="border rounded-lg px-3 py-2"
                            placeholder="Email"
                            value={c.email}
                            onChange={(e) => {
                              const next = [...createForm.correos];
                              next[idx] = { ...c, email: e.target.value };
                              setCreateForm({ ...createForm, correos: next });
                            }}
                          />
                          <input
                            className="border rounded-lg px-3 py-2"
                            placeholder="Etiqueta (ej: Personal)"
                            value={c.etiqueta}
                            onChange={(e) => {
                              const next = [...createForm.correos];
                              next[idx] = { ...c, etiqueta: e.target.value };
                              setCreateForm({ ...createForm, correos: next });
                            }}
                          />
                          <label className="flex items-center gap-2 text-sm">
                            <input
                              type="radio"
                              checked={c.esPrincipal}
                              onChange={() =>
                                setCreateForm({ ...createForm, correos: onlyOnePrincipal(createForm.correos, idx) })
                              }
                            />
                            Principal
                          </label>
                          <button
                            className="border rounded-lg px-3 py-2 hover:bg-slate-50"
                            onClick={() => {
                              const next = createForm.correos.filter((_: any, i: number) => i !== idx);
                              setCreateForm({ ...createForm, correos: next });
                            }}
                          >
                            Quitar
                          </button>
                        </div>
                      ))}
                      {createForm.correos.length === 0 && (
                        <p className="text-sm text-slate-500">Agrega al menos un correo (si tu validator lo exige).</p>
                      )}
                    </div>
                  </div>

                  {/* Direcciones */}
                  <div className="rounded-xl border border-slate-200 p-4">
                    <div className="flex items-center justify-between mb-3">
                      <h3 className="font-semibold">Direcciones</h3>
                      <button onClick={addDireccion} className="text-sm px-3 py-1.5 rounded-lg bg-slate-900 text-white">
                        + Agregar
                      </button>
                    </div>

                    <div className="space-y-4">
                      {createForm.direcciones.map((d: Direccion, idx: number) => (
                        <div key={idx} className="rounded-xl border border-slate-200 p-3">
                          <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                            <input
                              className="border rounded-lg px-3 py-2"
                              placeholder="Calle principal"
                              value={d.callePrincipal}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, callePrincipal: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <input
                              className="border rounded-lg px-3 py-2"
                              placeholder="Calle secundaria"
                              value={d.calleSecundaria}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, calleSecundaria: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <input
                              className="border rounded-lg px-3 py-2"
                              placeholder="Etiqueta (Domicilio/Trabajo)"
                              value={d.etiqueta}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, etiqueta: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <input
                              className="border rounded-lg px-3 py-2"
                              placeholder="Ciudad"
                              value={d.ciudad}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, ciudad: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <input
                              className="border rounded-lg px-3 py-2"
                              placeholder="Provincia"
                              value={d.provincia}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, provincia: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <input
                              className="border rounded-lg px-3 py-2"
                              placeholder="País"
                              value={d.pais}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, pais: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <input
                              className="border rounded-lg px-3 py-2 md:col-span-2"
                              placeholder="Referencia"
                              value={d.referencia}
                              onChange={(e) => {
                                const next = [...createForm.direcciones];
                                next[idx] = { ...d, referencia: e.target.value };
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            />
                            <label className="flex items-center gap-2 text-sm">
                              <input
                                type="radio"
                                checked={d.esPrincipal}
                                onChange={() =>
                                  setCreateForm({
                                    ...createForm,
                                    direcciones: onlyOnePrincipal(createForm.direcciones, idx),
                                  })
                                }
                              />
                              Principal
                            </label>
                          </div>

                          <div className="mt-3 flex justify-end">
                            <button
                              className="text-sm px-3 py-1.5 rounded-lg border border-slate-200 hover:bg-slate-50"
                              onClick={() => {
                                const next = createForm.direcciones.filter((_: any, i: number) => i !== idx);
                                setCreateForm({ ...createForm, direcciones: next });
                              }}
                            >
                              Quitar dirección
                            </button>
                          </div>
                        </div>
                      ))}

                      {createForm.direcciones.length === 0 && (
                        <p className="text-sm text-slate-500">Agrega al menos una dirección (si tu validator lo exige).</p>
                      )}
                    </div>
                  </div>
                </>
              ) : (
                <>
                  {/* EDIT */}
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    {editForm.tipoPersonaId === 1 ? (
                      <>
                        <div>
                          <label className="text-sm font-medium">Nombres</label>
                          <input
                            className="mt-1 w-full border rounded-lg px-3 py-2"
                            value={editForm.nombres}
                            onChange={(e) => setEditForm({ ...editForm, nombres: e.target.value })}
                          />
                        </div>
                        <div>
                          <label className="text-sm font-medium">Apellidos</label>
                          <input
                            className="mt-1 w-full border rounded-lg px-3 py-2"
                            value={editForm.apellidos}
                            onChange={(e) => setEditForm({ ...editForm, apellidos: e.target.value })}
                          />
                        </div>
                      </>
                    ) : (
                      <>
                        <div className="md:col-span-2">
                          <label className="text-sm font-medium">Razón social</label>
                          <input
                            className="mt-1 w-full border rounded-lg px-3 py-2"
                            value={editForm.razonSocial}
                            onChange={(e) => setEditForm({ ...editForm, razonSocial: e.target.value })}
                          />
                        </div>
                        <div className="md:col-span-2">
                          <label className="text-sm font-medium">Nombre comercial</label>
                          <input
                            className="mt-1 w-full border rounded-lg px-3 py-2"
                            value={editForm.nombreComercial}
                            onChange={(e) => setEditForm({ ...editForm, nombreComercial: e.target.value })}
                          />
                        </div>
                      </>
                    )}
                  </div>
                </>
              )}
            </div>

            <div className="flex items-center justify-end gap-2 p-5 border-t border-slate-100">
              <button onClick={() => setOpen(false)} className="px-4 py-2 rounded-lg border border-slate-200 hover:bg-slate-50">
                Cancelar
              </button>
              <button onClick={save} className="px-4 py-2 rounded-lg bg-slate-900 text-white hover:bg-slate-800">
                Guardar
              </button>
            </div>
          </div>
        </div>
      )}
    </main>
  );
}
