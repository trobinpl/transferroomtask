import { getRouteApi, Link } from "@tanstack/react-router"
import { type FC } from "react"

const people = [
  {
    name: "Leslie Alexander",
    email: "leslie.alexander@example.com",
    role: "Co-Founder / CEO",
    imageUrl:
      "https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80",
    teamId: 1,
    lastSeen: "3h ago",
    lastSeenDateTime: "2023-01-23T13:23Z",
  },
  {
    name: "Michael Foster",
    email: "michael.foster@example.com",
    role: "Co-Founder / CTO",
    imageUrl:
      "https://images.unsplash.com/photo-1519244703995-f4e0f30006d5?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80",
    teamId: 2,
    lastSeen: "3h ago",
    lastSeenDateTime: "2023-01-23T13:23Z",
  },
]

const route = getRouteApi("/")

export const AllTeams: FC = () => {
  const { availableTeams } = route.useLoaderData()
  return (
    <ul role="list" className="divide-y divide-gray-100">
      {availableTeams!.map(team => (
        <li
          key={team.id}
          className="relative flex justify-between gap-x-6 px-4 py-5 hover:bg-gray-50 sm:px-6 lg:px-8"
        >
          <div className="flex min-w-0 gap-x-4">
            {team.crest && (
              <img
                alt=""
                src={team.crest}
                className="size-12 flex-none rounded-full bg-gray-50"
              />
            )}
            <div className="min-w-0 flex-auto">
              <p className="text-sm/6 font-semibold text-gray-900">
                <Link to="/team/$id" params={{ id: `${team.id}` }}>
                  <span className="absolute inset-x-0 -top-px bottom-0" />
                  {team.name}
                </Link>
              </p>
            </div>
          </div>
        </li>
      ))}
    </ul>
  )
}
