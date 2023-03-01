export class AudienceDto {
  public constructor(
    public readonly audienceName: string,
    public readonly displayName : string,
  ) { }
}

export class GetAudiencesResponseDto {
  public constructor(
    public readonly audiences: AudienceDto[],
  ) { }
}
