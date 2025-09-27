import { getRouteApi, Link } from "@tanstack/react-router"
import { type FC } from "react"
import { HomeIcon } from "@heroicons/react/20/solid"

const route = getRouteApi("/team/$id")

export const SpecificTeam: FC = () => {
  const { team } = route.useLoaderData()
  const { id } = route.useParams()
  return (
    <>
      <h2 className="text-2xl/7 font-bold text-gray-900 sm:truncate sm:text-3xl sm:tracking-tight">
        Team {team.name}
      </h2>
      <nav aria-label="Breadcrumb" className="flex">
        <ol role="list" className="flex items-center space-x-4 mt-4">
          <li>
            <div>
              <Link to="/" className="text-gray-400 hover:text-gray-500">
                <HomeIcon aria-hidden="true" className="size-5 shrink-0" />
                <span className="sr-only">Home</span>
              </Link>
            </div>
          </li>
          <li>
            <div className="flex items-center">
              <svg
                fill="currentColor"
                viewBox="0 0 20 20"
                aria-hidden="true"
                className="size-5 shrink-0 text-gray-300"
              >
                <path d="M5.555 17.776l8-16 .894.448-8 16-.894-.448z" />
              </svg>
              <Link
                to="/team/$id"
                params={{ id: id }}
                aria-current="page"
                className="ml-4 text-sm font-medium text-gray-500 hover:text-gray-700"
              >
                {team.name}
              </Link>
            </div>
          </li>
        </ol>
      </nav>
      <ul role="list" className="divide-y divide-gray-100">
        {team?.squad?.map(player => (
          <li key={player.name} className="flex justify-between gap-x-6 py-5">
            <div className="flex min-w-0 gap-x-4">
              {player.profilePictureUrl &&
                player.profilePictureUrl !== "unknown" && (
                  <img
                    alt={`Profile picture of ${player.name}`}
                    src={player.profilePictureUrl}
                    className="size-12 flex-none rounded-full bg-gray-50"
                  />
                )}
              <div className="min-w-0 flex-auto">
                <p className="text-sm/6 font-semibold text-gray-900">
                  {player.name}{" "}
                  <span className="text-xs/5 text-gray-500 font-normal">
                    {player.dateOfBirth}
                  </span>
                </p>
                <p className="mt-1 truncate text-xs/5 text-gray-500">
                  {player.position}
                </p>
              </div>
            </div>
          </li>
        ))}
      </ul>
    </>
  )
}
