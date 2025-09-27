import { useMemo } from "react"
import type { paths } from "../premierroom"
import createClient from "openapi-fetch"
import { ApiClientContext } from "./ApiClientContext"

interface ApiContextProviderProps {
  children: React.ReactNode
}

export const ApiContextProvider: React.FC<ApiContextProviderProps> = ({
  children,
}) => {
  const client = useMemo(
    () =>
      createClient<paths>({
        baseUrl: import.meta.env.VITE_API_BASE_URL || "https://localhost:9091",
        credentials: "include",
      }),
    []
  )
  return (
    <ApiClientContext.Provider value={client}>
      {children}
    </ApiClientContext.Provider>
  )
}
