"use client";

import { useEffect, useState } from "react";
import { sociosService } from "@/app/services/socios.service";
import { cuentasService } from "@/app/services/cuentas.service";
import { transferenciasService } from "@/app/services/transferencias.service";

export default function TransferenciasPage() {
  const [socios, setSocios] = useState<any[]>([]);
  const [cuentasOrigen, setCuentasOrigen] = useState<any[]>([]);
  const [cuentasDestino, setCuentasDestino] = useState<any[]>([]);

  const [socioOrigenId, setSocioOrigenId] = useState("");
  const [socioDestinoId, setSocioDestinoId] = useState("");

  const [cuentaOrigenId, setCuentaOrigenId] = useState("");
  const [cuentaDestinoId, setCuentaDestinoId] = useState("");

  const [monto, setMonto] = useState("");
  const [observacion, setObservacion] = useState("");

  const [loading, setLoading] = useState(false);

  /* ================= DATA ================= */
  const loadSocios = async () => {
    const res: any = await sociosService.getAll();
    setSocios(res.data ?? res);
  };

  const loadCuentasOrigen = async (id: string) => {
    const res: any = await cuentasService.getBySocio(id);
    setCuentasOrigen(res.data ?? res);
  };

  const loadCuentasDestino = async (id: string) => {
    const res: any = await cuentasService.getBySocio(id);
    setCuentasDestino(res.data ?? res);
  };

  useEffect(() => {
    loadSocios();
  }, []);

  /* ================= ACTION ================= */
  const transferir = async () => {
    if (!cuentaOrigenId || !cuentaDestinoId || !monto) {
      alert("Complete todos los campos");
      return;
    }

    if (cuentaOrigenId === cuentaDestinoId) {
      alert("La cuenta origen y destino no pueden ser la misma");
      return;
    }

    setLoading(true);
    try {
      await transferenciasService.transferir({
        cuentaOrigenId,
        cuentaDestinoId,
        monto: Number(monto),
        observacion,
      });

      alert("Transferencia realizada correctamente");

      setMonto("");
      setObservacion("");
    } catch (e: any) {
      alert(e?.message || "Error al realizar la transferencia");
    } finally {
      setLoading(false);
    }
  };

  /* ================= UI ================= */
  return (
    <main className="min-h-screen bg-slate-100 p-10">
      <div className="max-w-2xl mx-auto bg-white rounded-xl shadow border p-8">

        <h1 className="text-xl font-semibold mb-6">Transferencias</h1>

        {/* ORIGEN */}
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="text-sm font-medium">Socio Origen</label>
            <select
              className="w-full border rounded px-3 py-2"
              value={socioOrigenId}
              onChange={(e) => {
                setSocioOrigenId(e.target.value);
                setCuentaOrigenId("");
                loadCuentasOrigen(e.target.value);
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
          </div>

          <div>
            <label className="text-sm font-medium">Cuenta Origen</label>
            <select
              className="w-full border rounded px-3 py-2"
              value={cuentaOrigenId}
              disabled={!socioOrigenId}
              onChange={(e) => setCuentaOrigenId(e.target.value)}
            >
              <option value="">Seleccione cuenta</option>
              {cuentasOrigen.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.numeroCuenta} — ${c.saldo.toFixed(2)}
                </option>
              ))}
            </select>
          </div>
        </div>

        {/* DESTINO */}
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="text-sm font-medium">Socio Destino</label>
            <select
              className="w-full border rounded px-3 py-2"
              value={socioDestinoId}
              onChange={(e) => {
                setSocioDestinoId(e.target.value);
                setCuentaDestinoId("");
                loadCuentasDestino(e.target.value);
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
          </div>

          <div>
            <label className="text-sm font-medium">Cuenta Destino</label>
            <select
              className="w-full border rounded px-3 py-2"
              value={cuentaDestinoId}
              disabled={!socioDestinoId}
              onChange={(e) => setCuentaDestinoId(e.target.value)}
            >
              <option value="">Seleccione cuenta</option>
              {cuentasDestino.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.numeroCuenta}
                </option>
              ))}
            </select>
          </div>
        </div>

        {/* MONTO */}
        <label className="text-sm font-medium">Monto</label>
        <input
          type="number"
          min={0}
          className="w-full border rounded px-3 py-2 mb-4"
          value={monto}
          onChange={(e) => setMonto(e.target.value)}
        />

        {/* OBSERVACIÓN */}
        <label className="text-sm font-medium">Observación</label>
        <input
          className="w-full border rounded px-3 py-2 mb-6"
          value={observacion}
          onChange={(e) => setObservacion(e.target.value)}
          placeholder="Transferencia entre cuentas"
        />

        {/* ACTION */}
        <button
          onClick={transferir}
          disabled={loading}
          className="w-full bg-slate-900 text-white py-2 rounded"
        >
          {loading ? "Procesando..." : "Realizar Transferencia"}
        </button>
      </div>
    </main>
  );
}
