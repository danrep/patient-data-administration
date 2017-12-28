CREATE FUNCTION Function_GetSiteId
(
	@site_code varchar(50)
)
RETURNS int
AS
BEGIN
	declare @site_id int
	select @site_id = id from Administration_SiteInformation where SiteCode = @site_code and IsDeleted = 'false'
	return @site_id
END
