import { defineConfig } from "vite"
import react from "@vitejs/plugin-react"
import tailwindcss from "@tailwindcss/vite"
import { tanstackRouter } from "@tanstack/router-plugin/vite"
import basicSsl from "@vitejs/plugin-basic-ssl"

// https://vite.dev/config/
export default defineConfig({
  server: {
    port: 9092,
  },
  plugins: [
    tanstackRouter({
      target: "react",
      autoCodeSplitting: true,
    }),
    react(),
    tailwindcss(),
    basicSsl(),
  ],
})
