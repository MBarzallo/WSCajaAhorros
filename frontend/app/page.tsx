"use client";

import Link from "next/link";

export default function Home() {
  return (
    <main className="min-h-screen bg-slate-50 text-slate-800">

      {/* NAVBAR */}
      <nav className="fixed top-0 w-full z-50 backdrop-blur bg-white/70 border-b border-slate-200">
        <div className="max-w-7xl mx-auto px-8 py-4 flex justify-between items-center">
          <div className="flex flex-row">

            <span className="text-lg font-bold tracking-tight text-slate-900">
              Caja de Ahorros
            </span>
          </div>

          <div className="hidden md:flex gap-8 text-sm font-medium text-slate-600">
            <a href="#servicios" className="hover:text-slate-900">Servicios</a>
            <a href="#nosotros" className="hover:text-slate-900">Proyecto</a>
            <a href="#beneficios" className="hover:text-slate-900">Beneficios</a>
            <a href="#contacto" className="hover:text-slate-900">Contacto</a>
          </div>

          <Link
            href="/auth/login"
            className="px-5 py-2 rounded-lg bg-slate-900 text-white text-sm font-medium hover:bg-slate-800 transition"
          >
            Iniciar Sesión
          </Link>
        </div>
      </nav>

      {/* HERO */}
      <section className="pt-36 pb-28 bg-gradient-to-br from-slate-900 via-slate-800 to-slate-700 text-white">
        <div className="max-w-5xl mx-auto px-8 text-center">
          <h1 className="text-4xl md:text-5xl font-semibold leading-tight mb-6">
            Sistema de Gestión para<br />Caja de Ahorros
          </h1>
          <p className="text-lg md:text-xl text-slate-300 max-w-3xl mx-auto">
            Plataforma interna para la administración de socios, cuentas de ahorro,
            depósitos, retiros y transferencias internas dentro de la institución.
          </p>
        </div>
      </section>

      {/* STATS */}
      <section className="-mt-20 relative z-10">
        <div className="max-w-6xl mx-auto px-8 grid grid-cols-2 md:grid-cols-4 gap-6">
          {[
            ["Socios", "25+"],
            ["Cuentas de Ahorro", "30+"],
            ["Movimientos", "120+"],
            ["Trazabilidad", "100%"],
          ].map(([label, value], i) => (
            <div
              key={i}
              className="bg-white/80 backdrop-blur rounded-2xl p-6 shadow-lg border border-slate-200 text-center"
            >
              <p className="text-3xl font-semibold text-slate-900">{value}</p>
              <p className="text-sm text-slate-500 mt-1">{label}</p>
            </div>
          ))}
        </div>
      </section>

      {/* SERVICIOS */}
      <section id="servicios" className="py-28">
        <div className="max-w-6xl mx-auto px-8">
          <h2 className="text-3xl font-semibold text-center mb-14">
            Funcionalidades del Sistema
          </h2>

          <div className="grid md:grid-cols-3 gap-10">
            {[
              {
                title: "Control de Accesos",
                desc: "Gestión de usuarios, roles y permisos para garantizar un acceso seguro al sistema.",
              },
              {
                title: "Gestión de Socios",
                desc: "Administración de socios y sus cuentas de ahorro con información centralizada.",
              },
              {
                title: "Operaciones de Ahorro",
                desc: "Registro controlado de depósitos, retiros y transferencias internas entre socios.",
              },
            ].map((item, i) => (
              <div
                key={i}
                className="bg-white rounded-2xl p-8 shadow-md border border-slate-200 hover:shadow-xl transition"
              >
                <h3 className="text-lg font-semibold mb-3">{item.title}</h3>
                <p className="text-slate-600 text-sm leading-relaxed">
                  {item.desc}
                </p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* ABOUT */}
      <section id="nosotros" className="py-24 bg-slate-100">
        <div className="max-w-4xl mx-auto px-8 text-center">
          <h2 className="text-3xl font-semibold mb-6">Sobre el Proyecto</h2>
          <p className="text-slate-700 leading-relaxed">
            Proyecto académico desarrollado por estudiantes de la Universidad
            Politécnica Salesiana, orientado a la digitalización y control de los
            procesos internos de una Caja de Ahorros, priorizando la transparencia,
            seguridad y correcta gestión de la información financiera.
          </p>
        </div>
      </section>

      {/* BENEFICIOS */}
      <section id="beneficios" className="py-28">
        <div className="max-w-6xl mx-auto px-8">
          <h2 className="text-3xl font-semibold text-center mb-14">
            Beneficios Institucionales
          </h2>

          <div className="grid md:grid-cols-4 gap-6 text-sm">
            {[
              "Automatización de procesos manuales",
              "Control y trazabilidad de movimientos",
              "Acceso seguro para socios y personal",
              "Gestión centralizada de la información",
            ].map((b, i) => (
              <div
                key={i}
                className="bg-white rounded-xl p-6 shadow border border-slate-200 text-center"
              >
                {b}
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CONTACTO */}
      <section id="contacto" className="py-24 bg-slate-100">
        <div className="max-w-6xl mx-auto px-8">
          <h2 className="text-3xl font-semibold text-center mb-12">Equipo</h2>

          <div className="grid md:grid-cols-4 gap-6 text-center text-sm">
            {[
              "Mateo Barzallo",
              "Jorge Cueva",
              "Karen Quito",
              "Jennyfer Ramírez",
            ].map((name, i) => (
              <div
                key={i}
                className="bg-white rounded-xl p-6 shadow border border-slate-200"
              >
                {name}
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* FOOTER */}
      <footer className="bg-slate-900 text-slate-400 py-10 text-center text-sm">
        <p>
          Sistema de Gestión para Caja de Ahorros – Proyecto Académico UPS
        </p>
        <p className="mt-2">
          © 2026 Todos los derechos reservados
        </p>
      </footer>

    </main>
  );
}
