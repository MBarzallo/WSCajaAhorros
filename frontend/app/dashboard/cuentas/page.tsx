"use client";

import { useEffect, useState } from "react";
import { cuentasService, productosCuentaService } from "@/app/services/cuentas.service";
import { sociosService } from "@/app/services/socios.service";

export default function CuentasPage() {
  const [socios, setSocios] = useState<any[]>([]);
  const [productos, setProductos] = useState<any[]>([]);
  const [cuentas, setCuentas] = useState<any[]>([]);
  const [socioId, setSocioId] = useState("");

  const [showModal, setShowModal] = useState(false);
  const [productoCuentaId, setProductoCuentaId] = useState("");

  /* ================= DATA ================= */
  const loadSocios = async () => {
    const res: any = await sociosService.getAll();
    setSocios(res.data ?? res);
  };

  const loadProductos = async () => {
    const res: any = await productosCuentaService.getAll();
    setProductos(res.data ?? res);
  };

  const loadCuentas = async (id: string) => {
    const res: any = await cuentasService.getBySocio(id);
    setCuentas(res.data ?? res);
  };

  useEffect(() => {
    loadSocios();
    loadProductos();
  }, []);

  /* ================= ACTIONS ================= */
  const crearCuenta = async () => {
    try {
      await cuentasService.create({
        socioId,
        productoCuentaId,
      });
      setShowModal(false);
      loadCuentas(socioId);
    } catch {
      alert("Error al crear cuenta");
    }
  };

  const bloquearCuenta = async (id: string) => {
    await cuentasService.bloquear(id);
    loadCuentas(socioId);
  };

  const cerrarCuenta = async (id: string) => {
    if (!confirm("¿Cerrar cuenta definitivamente?")) return;
    await cuentasService.cerrar(id);
    loadCuentas(socioId);
  };

  /* ================= UI ================= */
  return (
    <main className="min-h-screen bg-slate-100 p-10">
      <div className="max-w-7xl mx-auto bg-white rounded-xl shadow border p-8">

        <h1 className="text-xl font-semibold mb-6">Cuentas</h1>

        {/* SELECT SOCIO */}
        <div className="flex gap-4 mb-6">
          <select
            className="border rounded px-3 py-2 w-80"
            value={socioId}
            onChange={(e) => {
              setSocioId(e.target.value);
              loadCuentas(e.target.value);
            }}
          >
            <option value="">Seleccione un socio</option>
            {socios.map((s) => (
              <option key={s.id} value={s.id}>
                {s.tipoPersona === 1
                  ? `${s.nombres} ${s.apellidos}`
                  : s.razonSocial}
              </option>
            ))}
          </select>

          {socioId && (
            <button
              onClick={() => setShowModal(true)}
              className="bg-slate-900 text-white px-4 rounded"
            >
              + Nueva Cuenta
            </button>
          )}
        </div>

        {/* TABLE */}
        <table className="w-full border text-sm">
          <thead className="bg-slate-50">
            <tr>
              <th className="p-3">Número</th>
              <th className="p-3">Saldo</th>
              <th className="p-3">Estado</th>
              <th className="p-3">Fecha Apertura</th>
              <th className="p-3">Acciones</th>
            </tr>
          </thead>
          <tbody>
            {cuentas.map((c) => (
              <tr key={c.id} className="border-t">
                <td className="p-3">{c.numeroCuenta}</td>
                <td className="p-3">${c.saldo.toFixed(2)}</td>
                <td className="p-3">{c.estado}</td>
                <td className="p-3">
                  {new Date(c.fechaApertura).toLocaleDateString()}
                </td>
                <td className="p-3 flex gap-2 justify-center">
                  {c.estado === "Activa" && (
                    <button
                      onClick={() => bloquearCuenta(c.id)}
                      className="text-yellow-600"
                    >
                      Bloquear
                    </button>
                  )}
                  {c.estado !== "Cerrada" && (
                    <button
                      onClick={() => cerrarCuenta(c.id)}
                      className="text-red-600"
                    >
                      Cerrar
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        {/* MODAL */}
        {showModal && (
          <div className="fixed inset-0 bg-black/40 flex items-center justify-center">
            <div className="bg-white p-6 rounded-xl w-96">
              <h2 className="font-semibold mb-4">Crear Cuenta</h2>

              <select
                className="w-full border rounded px-3 py-2 mb-4"
                value={productoCuentaId}
                onChange={(e) => setProductoCuentaId(e.target.value)}
              >
                <option value="">Seleccione producto</option>
                {productos.map((p) => (
                  <option key={p.id} value={p.id}>
                    {p.nombre} ({p.codigo})
                  </option>
                ))}
              </select>

              <div className="flex justify-end gap-2">
                <button
                  onClick={() => setShowModal(false)}
                  className="px-4 py-2 border rounded"
                >
                  Cancelar
                </button>
                <button
                  onClick={crearCuenta}
                  className="px-4 py-2 bg-slate-900 text-white rounded"
                >
                  Crear
                </button>
              </div>
            </div>
          </div>
        )}
      </div>
    </main>
  );
}
