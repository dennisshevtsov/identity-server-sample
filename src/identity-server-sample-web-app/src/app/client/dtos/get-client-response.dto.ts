export class GetClientResponseDto {
  public constructor(
    public clientName      : string = '',
    public displayName     : string = '',
    public description     : string = '',
    public scopes          : string[] = [],
    public redirectUris    : string[] = [],
    public postRedirectUris: string[] = [],
    public corsOrigins     : string[] = [],
  ) {}
}
