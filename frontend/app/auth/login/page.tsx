"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { authService } from "../../services/auth.service";
import { Logo } from "../../components/Logo";

export default function Login() {
  const router = useRouter();

  const [usuario, setUsuario] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e?: React.FormEvent) => {
    e?.preventDefault();
    setLoading(true);

    try {
      const response: any = await authService.login({
        nombreUsuario: usuario,
        contrasena: password,
      });

      if (response.ok) {
        localStorage.setItem("token", response.data.accessToken);
        if (response.data.usuario?.id)
          localStorage.setItem("userId", response.data.usuario.id);
        if (response.data.usuario?.nombreUsuario)
          localStorage.setItem("userName", response.data.usuario.nombreUsuario);

        router.push("/dashboard");
      } else {
        throw new Error("Credenciales incorrectas");
      }
    } catch (error) {
      console.error(error);
      alert("Credenciales incorrectas");
    } finally {
      setLoading(false);
    }
  };

  return (
    <main className="min-h-screen flex items-center justify-center bg-gradient-to-br from-[#204e96] via-[#5ec0ea] to-[#d3ab78] px-4">
      <div className="w-full max-w-md bg-[#fefefe] rounded-2xl shadow-2xl p-10">

        {/* LOGO */}
        <div className="flex flex-col items-center mb-8">
          <Logo h={80} w={80} bg />
          <h1 className="mt-4 text-2xl font-semibold text-[#204e96]">
            Shell
          </h1>
          <p className="text-sm text-gray-600 mt-1">
            Sistema de Gestión – Caja de Ahorros
          </p>
        </div>

        {/* FORM */}
        <form onSubmit={handleSubmit} className="space-y-6">

          {/* Usuario */}
          <div>
            <label className="block text-sm font-medium text-[#204e96] mb-1">
              Nombre de usuario
            </label>
            <div className="relative">
              <span className="absolute inset-y-0 left-3 flex items-center text-gray-400">
                <i className="ri-user-line"></i>
              </span>
              <input
                type="text"
                required
                value={usuario}
                onChange={(e) => setUsuario(e.target.value)}
                placeholder="usuario"
                className="w-full pl-10 pr-4 py-3 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-[#5ec0ea]"
              />
            </div>
          </div>

          {/* Password */}
          <div>
            <label className="block text-sm font-medium text-[#204e96] mb-1">
              Contraseña
            </label>
            <div className="relative">
              <span className="absolute inset-y-0 left-3 flex items-center text-gray-400">
                <i className="ri-lock-line"></i>
              </span>
              <input
                type={showPassword ? "text" : "password"}
                required
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="••••••••"
                className="w-full pl-10 pr-10 py-3 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-[#5ec0ea]"
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute inset-y-0 right-3 flex items-center text-gray-400 hover:text-[#204e96]"
              >
                <i className={showPassword ? "ri-eye-off-line" : "ri-eye-line"} />
              </button>
            </div>
          </div>

          {/* Forgot */}
          {/* <div className="text-right">
            <Link
              href="/forgot-password"
              className="text-sm text-[#5ec0ea] hover:text-[#204e96]"
            >
              ¿Olvidaste tu contraseña?
            </Link>
          </div> */}

          {/* Submit */}
          <button
            type="submit"
            disabled={loading}
            className="w-full py-3 rounded-lg bg-[#204e96] text-white font-medium hover:bg-[#5ec0ea] transition disabled:opacity-60"
          >
            {loading ? "Verificando..." : "Acceder al Sistema"}
          </button>
        </form>

        {/* FOOTER */}
        <p className="text-xs text-center text-gray-400 mt-8">
          Acceso exclusivo para personal autorizado
        </p>
      </div>
    </main>
  );
}