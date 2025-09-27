import { queryOptions } from "@tanstack/react-query"
import type { ApiClient } from "./ApiClientContext/ApiClientContext"

export const AvailableTeamsQueryKey = ["teams"]

export const getAllAvailableTeamsQueryOptions = (api: ApiClient) =>
  queryOptions({
    queryKey: AvailableTeamsQueryKey,
    queryFn: () => getAllAvailableTeams(api),
  })

export const getAllAvailableTeams = async (api: ApiClient) => {
  const result = await api.GET("/teams/all")

  return result.data?.data.availableTeams
}
