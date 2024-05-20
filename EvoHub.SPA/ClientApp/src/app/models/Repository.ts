export interface Repository {
  id: number
  name: string
  owner: Owner
  description?: string
  url: string
  language: string
  updatedAt: Date
}

export interface Owner {
  login: string
}

export function createEmptyRepository(): Repository {
  return {
    id: 0,
    name: "default name",
    owner: createEmptyOwner(),
    description: "default description",
    url: "defaulturl.com",
    language: "default language",
    updatedAt: new Date(0),
  };
}

function createEmptyOwner(): Owner {
  return {
    login: "default login"
  };
}
