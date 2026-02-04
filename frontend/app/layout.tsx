import Head from "next/head";
import { inter } from "./fonts";
import "./ui/stylesheets/globals.css";

export const metadata = {
  title: "Home",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="es">
      <head>
         <link href="https://cdn.jsdelivr.net/npm/remixicon@4.2.0/fonts/remixicon.css" rel="stylesheet"></link>
      </head>
      <body className={`${inter.className} antialiased`}>{children}</body>
    </html>
  );
}
