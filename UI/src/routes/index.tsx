import { createFileRoute } from "@tanstack/react-router"
import { AllTeams } from "../teams/AllTeams"
import { getAllAvailableTeamsQueryOptions } from "../api/teams"

export const Route = createFileRoute("/")({
  loader: async ({ context }) => {
    const { api, queryClient } = context
    const availableTeams = await queryClient.ensureQueryData(
      getAllAvailableTeamsQueryOptions(api)
    )

    return {
      availableTeams,
    }
  },
  component: () => <AllTeams />,
})
