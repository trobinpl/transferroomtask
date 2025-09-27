import { queryOptions } from "@tanstack/react-query"
import type { ApiClient } from "./ApiClientContext/ApiClientContext"

const AvailableTeamsQueryKey = ["teams"]

export const getAllAvailableTeamsQueryOptions = (api: ApiClient) =>
  queryOptions({
    queryKey: AvailableTeamsQueryKey,
    queryFn: () => getAllAvailableTeams(api),
  })

export const getAllAvailableTeams = async (api: ApiClient) => {
  const result = await api.GET("/teams/all")

  return result.data?.data.availableTeams
}

const GetTeamByIdQueryKey = (teamId: number) => ["team", teamId]

export const getTeamByIdQueryOptions = (api: ApiClient, teamId: number) =>
  queryOptions({
    queryKey: GetTeamByIdQueryKey(teamId),
    queryFn: () => getTeamById(api, teamId),
  })

export const getTeamById = async (api: ApiClient, teamId: number) => {
  const result = await api.GET("/teams/getbyid/{teamId}", {
    params: {
      path: {
        teamId,
      },
    },
  })

  return result.data?.data
}
