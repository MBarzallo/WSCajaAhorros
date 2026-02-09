import Link from "next/link";
import { Logo } from "../components/Logo";

export const metadata = {
  title: "Autenticación | Shell",
};

export default function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <main className="">

          {children}
    </main>
  );
}
