import { useContext } from "react"
import { ApiClientContext } from "./ApiClientContext"

export const useApiClient = () => {
  const client = useContext(ApiClientContext)

  if (!client) {
    throw new Error("useApiClient must be used within ApiContextProvider")
  }

  return client
}
