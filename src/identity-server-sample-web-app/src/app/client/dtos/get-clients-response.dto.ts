export class ClientDto {
  public constructor(
    public readonly name       : string,
    public readonly displayName: string,
  ) { }
}

export class GetClientsResponseDto {
  public constructor(
    public clients: ClientDto[],
  ) { }
}
