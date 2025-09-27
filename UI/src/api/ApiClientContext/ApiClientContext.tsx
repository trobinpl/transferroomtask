import type createClient from "openapi-fetch"
import { createContext } from "react"
import type { paths } from "../premierroom"

export type ApiClient = ReturnType<typeof createClient<paths>>

export const ApiClientContext = createContext<ApiClient | null>(null)
