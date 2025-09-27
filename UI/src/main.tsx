import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import "./index.css"
import { ApiContextProvider } from "./api/ApiClientContext/ApiClientContextProvider"
import { App } from "./App"

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ApiContextProvider>
      <App />
    </ApiContextProvider>
  </StrictMode>
)
