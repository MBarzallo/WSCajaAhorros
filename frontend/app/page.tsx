"use client";

import Link from "next/link";

export default function Home() {
  return (
    <main className="min-h-screen bg-[#fefefe] text-[#204e96]">

      {/* NAVBAR */}
      <nav className="fixed top-0 w-full z-50 backdrop-blur bg-[#fefefe]/80 border-b border-[#5ec0ea]/30">
        <div className="max-w-7xl mx-auto px-8 py-4 flex justify-between items-center">

          <span className="text-lg font-bold tracking-tight text-[#204e96]">
            Caja de Ahorros
          </span>

          <div className="hidden md:flex gap-8 text-sm font-medium text-[#204e96]/80">
            <a href="#servicios" className="hover:text-[#204e96]">Servicios</a>
            <a href="#nosotros" className="hover:text-[#204e96]">Proyecto</a>
            <a href="#beneficios" className="hover:text-[#204e96]">Beneficios</a>
            <a href="#contacto" className="hover:text-[#204e96]">Contacto</a>
          </div>

          <Link
            href="/auth/login"
            className="px-5 py-2 rounded-lg bg-[#204e96] text-[#fefefe] text-sm font-medium hover:bg-[#5ec0ea] transition"
          >
            Iniciar Sesión
          </Link>
        </div>
      </nav>

      {/* HERO */}
      <section className="pt-36 pb-28 bg-gradient-to-br from-[#204e96] via-[#1f5fa8] to-[#5ec0ea] text-[#fefefe]">
        <div className="max-w-5xl mx-auto px-8 text-center">
          <h1 className="text-4xl md:text-5xl font-semibold leading-tight mb-6">
            Sistema de Gestión para<br />Caja de Ahorros
          </h1>
          <p className="text-lg md:text-xl text-[#fefefe]/90 max-w-3xl mx-auto">
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
              className="bg-[#fefefe]/90 backdrop-blur rounded-2xl p-6 shadow-lg border border-[#d3ab78]/40 text-center"
            >
              <p className="text-3xl font-semibold text-[#204e96]">{value}</p>
              <p className="text-sm text-[#204e96]/70 mt-1">{label}</p>
            </div>
          ))}
        </div>
      </section>

      {/* SERVICIOS */}
      <section id="servicios" className="py-28">
        <div className="max-w-6xl mx-auto px-8">
          <h2 className="text-3xl font-semibold text-center mb-14 text-[#204e96]">
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
                className="bg-[#fefefe] rounded-2xl p-8 shadow-md border border-[#5ec0ea]/40 hover:shadow-xl transition"
              >
                <h3 className="text-lg font-semibold mb-3 text-[#204e96]">
                  {item.title}
                </h3>
                <p className="text-[#204e96]/75 text-sm leading-relaxed">
                  {item.desc}
                </p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* ABOUT */}
      <section id="nosotros" className="py-24 bg-[#5ec0ea]/10">
        <div className="max-w-4xl mx-auto px-8 text-center">
          <h2 className="text-3xl font-semibold mb-6 text-[#204e96]">
            Sobre el Proyecto
          </h2>
          <p className="text-[#204e96]/80 leading-relaxed">
            Proyecto académico desarrollado por estudiantes de la Universidad
            Politécnica Salesiana, orientado a la digitalización y control de los
            procesos internos de una Caja de Ahorros.
          </p>
        </div>
      </section>

      {/* BENEFICIOS */}
      <section id="beneficios" className="py-28">
        <div className="max-w-6xl mx-auto px-8">
          <h2 className="text-3xl font-semibold text-center mb-14 text-[#204e96]">
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
                className="bg-[#fefefe] rounded-xl p-6 shadow border border-[#d3ab78]/40 text-center"
              >
                {b}
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CONTACTO */}
      <section id="contacto" className="py-24 bg-[#5ec0ea]/10">
        <div className="max-w-6xl mx-auto px-8">
          <h2 className="text-3xl font-semibold text-center mb-12 text-[#204e96]">
            Equipo
          </h2>

          <div className="grid md:grid-cols-4 gap-6 text-center text-sm">
            {[
              "Mateo Barzallo",
              "Jorge Cueva",
              "Karen Quito",
              "Jennyfer Ramírez",
            ].map((name, i) => (
              <div
                key={i}
                className="bg-[#fefefe] rounded-xl p-6 shadow border border-[#5ec0ea]/40"
              >
                {name}
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* FOOTER */}
      <footer className="bg-[#204e96] text-[#fefefe]/80 py-10 text-center text-sm">
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
