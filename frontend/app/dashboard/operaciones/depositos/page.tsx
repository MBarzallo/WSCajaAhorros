"use client";

import { useEffect, useState } from "react";
import { sociosService } from "@/app/services/socios.service";
import { cuentasService } from "@/app/services/cuentas.service";
import { movimientosService } from "@/app/services/movimientos.service";

export default function DepositosPage() {
  const [socios, setSocios] = useState<any[]>([]);
  const [cuentas, setCuentas] = useState<any[]>([]);

  const [socioId, setSocioId] = useState("");
  const [cuentaId, setCuentaId] = useState("");

  const [monto, setMonto] = useState("");
  const [descripcion, setDescripcion] = useState("");

  const [loading, setLoading] = useState(false);

  /* ================= DATA ================= */
  const loadSocios = async () => {
    const res: any = await sociosService.getAll();
    setSocios(res.data ?? res);
  };

  const loadCuentas = async (id: string) => {
    const res: any = await cuentasService.getBySocio(id);
    setCuentas(res.data ?? res);
  };

  useEffect(() => {
    loadSocios();
  }, []);

  /* ================= ACTION ================= */
  const depositar = async () => {
    if (!cuentaId || !monto) {
      alert("Seleccione cuenta y monto");
      return;
    }

    setLoading(true);
    try {
      await movimientosService.depositar({
        cuentaId,
        monto: Number(monto),
        descripcion,
      });

      alert("Depósito realizado correctamente");

      setMonto("");
      setDescripcion("");
    } catch {
      alert("Error al realizar el depósito");
    } finally {
      setLoading(false);
    }
  };

  /* ================= UI ================= */
  return (
    <main className="min-h-screen bg-slate-100 p-10">
      <div className="max-w-xl mx-auto bg-white rounded-xl shadow border p-8">

        <h1 className="text-xl font-semibold mb-6">Depósitos</h1>

        {/* SOCIO */}
        <label className="text-sm font-medium">Socio</label>
        <select
          className="w-full border rounded px-3 py-2 mb-4"
          value={socioId}
          onChange={(e) => {
            setSocioId(e.target.value);
            setCuentaId("");
            loadCuentas(e.target.value);
          }}
        >
          <option value="">Seleccione socio</option>
          {socios.map((s) => (
            <option key={s.id} value={s.id}>
              {s.tipoPersona === 1
                ? `${s.nombres} ${s.apellidos}`
                : s.razonSocial}
            </option>
          ))}
        </select>

        {/* CUENTA */}
        <label className="text-sm font-medium">Cuenta</label>
        <select
          className="w-full border rounded px-3 py-2 mb-4"
          value={cuentaId}
          disabled={!socioId}
          onChange={(e) => setCuentaId(e.target.value)}
        >
          <option value="">Seleccione cuenta</option>
          {cuentas.map((c) => (
            <option key={c.id} value={c.id}>
              {c.numeroCuenta} — Saldo: ${c.saldo.toFixed(2)}
            </option>
          ))}
        </select>

        {/* MONTO */}
        <label className="text-sm font-medium">Monto</label>
        <input
          type="number"
          className="w-full border rounded px-3 py-2 mb-4"
          value={monto}
          onChange={(e) => setMonto(e.target.value)}
          min={0}
        />

        {/* DESCRIPCIÓN */}
        <label className="text-sm font-medium">Descripción</label>
        <input
          className="w-full border rounded px-3 py-2 mb-6"
          value={descripcion}
          onChange={(e) => setDescripcion(e.target.value)}
          placeholder="Depósito en ventanilla"
        />

        {/* ACTION */}
        <button
          onClick={depositar}
          disabled={loading}
          className="w-full bg-slate-900 text-white py-2 rounded"
        >
          {loading ? "Procesando..." : "Realizar Depósito"}
        </button>
      </div>
    </main>
  );
}
