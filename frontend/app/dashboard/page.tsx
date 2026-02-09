"use client";

import Link from "next/link";

export default function Dashboard() {
  return (
    <main className="min-h-screen bg-slate-100 text-slate-800">

      {/* HEADER */}
      <header className="bg-white border-b border-slate-200">
        <div className="max-w-7xl mx-auto px-8 py-5 flex justify-between items-center">
          <div>
            <h1 className="text-xl font-semibold text-slate-900">
              Core Financiero
            </h1>
            <p className="text-sm text-slate-500">
              Sistema Central – Caja de Ahorros
            </p>
          </div>

          <button
            onClick={() => {
              localStorage.clear();
              window.location.href = "/auth/login";
            }}
            className="text-sm text-red-600 hover:text-red-700 font-medium"
          >
            Cerrar sesión
          </button>
        </div>
      </header>

      {/* CONTENT */}
      <section className="max-w-7xl mx-auto px-8 py-10">


        {/* MODULES */}
        <div className="bg-white rounded-xl border border-slate-200 shadow-sm p-8 mb-10">
          <h2 className="text-lg font-semibold mb-6">
            Módulos del Sistema
          </h2>

          <div className="grid md:grid-cols-5 gap-6">
            {[
              {
                title: "Usuarios",
                desc: "Gestión del personal autorizado del sistema.",
                href: "/dashboard/usuarios",
              },
              
              {
                title: "Socios",
                desc: "Administración de socios de la caja de ahorros.",
                href: "/dashboard/socios",
              },
              {
                title: "Cuentas",
                desc: "Control de cuentas de ahorro y sus movimientos.",
                href: "/dashboard/cuentas",
              },
              {
                title: "Depósitos",
                desc: "Registro de aportes y movimientos de ingreso.",
                href: "/dashboard/operaciones/depositos",
              },
              {
                title: "Retiros",
                desc: "Control de retiros realizados por socios.",
                href: "/dashboard/operaciones/retiros",
              },
              {
                title: "Transferencias",
                desc: "Transferencias internas entre socios.",
                href: "/dashboard/operaciones/transferencias",
              },
            ].map((mod, i) => (
              <Link
                key={i}
                href={mod.href}
                className="border border-slate-200 rounded-xl p-6 hover:bg-slate-50 hover:border-slate-400 transition"
              >
                <h3 className="font-semibold mb-2 text-slate-900">
                  {mod.title}
                </h3>
                <p className="text-sm text-slate-600">
                  {mod.desc}
                </p>
              </Link>
            ))}
          </div>
        </div>

        {/* ACTIVITY */}
        <div className="bg-white rounded-xl border border-slate-200 shadow-sm p-8">
          <h2 className="text-lg font-semibold mb-6">
            Últimas Operaciones
          </h2>

          <div className="space-y-4 text-sm">
            {[
              "Depósito registrado – Socio #12 – $200",
              "Retiro registrado – Socio #05 – $50",
              "Transferencia interna – Socio #08 → Socio #14 – $100",
            ].map((item, i) => (
              <div
                key={i}
                className="border-b border-slate-100 pb-3 text-slate-700"
              >
                {item}
              </div>
            ))}
          </div>
        </div>

      </section>
    </main>
  );
}
