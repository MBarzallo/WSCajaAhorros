"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import styles from "../../ui/stylesheets/Auth.module.css"
import { authService } from "../../services/auth.service";

// ========== COMPONENTE LOGIN ==========
export default function Login() {
  const router = useRouter();
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e?: React.FormEvent) => {
    e?.preventDefault(); // Prevent form submission if called from event
    setLoading(true);

    try {
      const response: any = await authService.login({ email, password });
      console.log("Login success", response);

      if (response && response.token) {
        localStorage.setItem('token', response.token);
        // Assuming response.user contains ID or response.userId exists
        // Adjust based on actual backend response structure. 
        // If the backend returns user object:
        if (response.user?.id) localStorage.setItem('userId', response.user.id);
        // If it returns just userId
        if (response.userId) localStorage.setItem('userId', response.userId);

        // Also save user name for display if available
        if (response.user?.nombre) localStorage.setItem('userName', response.user.nombre);

        router.push('/user');
      } else {
        throw new Error("No token received");
      }
    } catch (error) {
      console.error("Login failed", error);
      alert("Error al iniciar sesión");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <div className={styles.formContainer}>

        {/* Email */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Correo Electrónico</label>
          <div className={styles.inputWrapper}>
            <div className={styles.inputIcon}>
              <i className="ri-mail-line"></i>
            </div>
            <input
              required
              className={styles.input}
              placeholder="correo@ejemplo.com"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              name="email"
            />
          </div>
        </div>

        {/* Password */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Contraseña</label>
          <div className={styles.inputWrapper}>
            <div className={styles.inputIcon}>
              <i className="ri-lock-line"></i>
            </div>
            <input
              required
              className={`${styles.input} ${styles.inputWithButton}`}
              placeholder="••••••••"
              type={showPassword ? "text" : "password"}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              name="password"
            />
            <button
              type="button"
              className={styles.togglePassword}
              onClick={() => setShowPassword(!showPassword)}
            >
              <i className={showPassword ? "ri-eye-off-line" : "ri-eye-line"}></i>
            </button>
          </div>
        </div>

        {/* Remember & Forgot */}
        <div className={styles.rememberForgot}>
          <Link href="/forgot-password" className={styles.forgotLink}>
            ¿Olvidaste tu contraseña?
          </Link>
        </div>

        {/* Submit Button */}
        <button
          type="button"
          className={styles.submitButton}
          onClick={() => handleSubmit()}
          disabled={loading}
        >
          {loading ? "Cargando..." : "Iniciar Sesión"}
        </button>
      </div>

      {/* Register Link */}
      <div className={styles.alternativeAction}>
        <p>
          ¿No tienes una cuenta?{" "}
          <Link href="/auth/register" className={styles.alternativeLink}>
            Regístrate Gratis
          </Link>
        </p>
      </div>

    </>
  );
}