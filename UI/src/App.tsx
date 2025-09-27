import { RouterProvider } from "@tanstack/react-router"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { useApiClient } from "./api/ApiClientContext/useApiClient"
import { router } from "./router"

export const App = () => {
  const queryClient = new QueryClient()
  const api = useApiClient()

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider
        router={router}
        context={{ api: api, queryClient: queryClient }}
      />
    </QueryClientProvider>
  )
}
