import { createFileRoute, redirect } from "@tanstack/react-router"
import { getTeamByIdQueryOptions } from "../api/teams"
import { SpecificTeam } from "../teams/SpecificTeam"
import { Loader } from "../ui/Loader"

export const Route = createFileRoute("/team/$id")({
  loader: async ({ context, params }) => {
    const { api, queryClient } = context
    const { id } = params
    const team = await queryClient.ensureQueryData(
      getTeamByIdQueryOptions(api, parseInt(id))
    )

    if (team) {
      return {
        team,
      }
    }

    throw redirect({ to: "/" })
  },
  component: SpecificTeam,
  pendingComponent: Loader,
  pendingMs: 0,
  pendingMinMs: 100,
})
