export class UpdateClientRequestDto {
  public constructor(
    public readonly clientName      : string,
    public readonly displayName     : string,
    public readonly description     : string,
    public readonly scopes          : string,
    public readonly redirectUris    : string,
    public readonly postRedirectUris: string,
    public readonly corsOrigins     : string,
  ) {}
}
