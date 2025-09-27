import { MagnifyingGlassIcon } from "@heroicons/react/16/solid"
import { getRouteApi, Link } from "@tanstack/react-router"
import { useCallback, useMemo, useState, type FC } from "react"
import type { AvailableTeam } from "../api/teams"

const route = getRouteApi("/")

export const AllTeams: FC = () => {
  const { availableTeams } = route.useLoaderData()
  const [searchTerm, setSearchTerm] = useState<string>("")

  const searchTeam = useCallback((team: AvailableTeam, term: string) => {
    if (!term) return true

    const normalizedTerm = term.toLowerCase().trim()

    // Search in official name
    if (team.name.toLowerCase().includes(normalizedTerm)) {
      return true
    }

    // Search in short name
    if (team.shortName.toLowerCase().includes(normalizedTerm)) {
      return true
    }

    // Search in TLA (three-letter abbreviation)
    if (team.tla.toLowerCase().includes(normalizedTerm)) {
      return true
    }

    // Search in nicknames
    if (
      team.nicknames.some(nickname =>
        nickname.toLowerCase().includes(normalizedTerm)
      )
    ) {
      return true
    }

    return false
  }, [])

  const filteredTeams = useMemo(() => {
    return availableTeams.filter(team => {
      if (!searchTeam(team, searchTerm)) {
        return false
      }

      return true
    })
  }, [availableTeams, searchTeam, searchTerm])
  return (
    <>
      <div className="md:flex md:items-center md:justify-between">
        <div className="min-w-0 flex-1">
          <h2 className="text-2xl/7 font-bold text-gray-900 sm:truncate sm:text-3xl sm:tracking-tight">
            All Premiere League teams
          </h2>
        </div>
        <div className="mt-4 flex md:mt-0 md:ml-4">
          <div className="flex">
            <div className="-mr-px grid grow grid-cols-1 focus-within:relative">
              <input
                id="search"
                name="search"
                type="text"
                placeholder="The Gunners"
                value={searchTerm}
                onChange={e => setSearchTerm(e.target.value)}
                className="col-start-1 row-start-1 block w-full rounded-md bg-white py-1.5 pr-3 pl-10 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:pl-9 sm:text-sm/6"
              />
              <MagnifyingGlassIcon
                aria-hidden="true"
                onClick={() => setSearchTerm("")}
                className="pointer-events-none col-start-1 row-start-1 ml-3 size-5 self-center text-gray-400 sm:size-4"
              />
            </div>
          </div>
        </div>
      </div>
      <ul role="list" className="divide-y divide-gray-100 mt-6">
        {filteredTeams!.map(team => (
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
                  <span className="ml-2 inline-flex items-center rounded-md bg-gray-100 px-2 py-1 text-xs font-medium text-gray-600">
                    {team.tla}
                  </span>
                </p>
                <p className="mt-1 truncate text-xs/5 text-gray-500">
                  <span>{team.nicknames?.join(",")}</span>
                </p>
              </div>
            </div>
          </li>
        ))}
      </ul>
    </>
  )
}
