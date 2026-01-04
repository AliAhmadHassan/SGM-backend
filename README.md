# SGM Backend (Materials Management System)

## Overview
This backend orchestrates the materials management platform for tracking supplier invoices, returns, transfers, direct exits, divergences, RMAs, and installations across a distributed factory network. It exposes a secure, traceable API for branch offices, warehouse operators, and project teams so every material movement is auditable.

## Technology Stack
- `C# / ASP.NET Core 2.2` hosted with Kestrel plus IIS-friendly InProcess mode.
- `Entity Framework Core 2.2` against SQL Server 2017 via the `SgmDataContext`.
- JWT-based authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`) with tokens issued by `JwtTokenGeneratorService`.
- LDAP login (`Novell.Directory.Ldap`) syncs Active Directory users into the system.
- Documentation via `Swashbuckle.AspNetCore` swagger UI with a custom `AddHeaderParameter` helper for authorization headers.
- Structured logging with `Serilog` (daily rolling file sinks, filtering expressions) and optional `Sentry.Serilog` for crash aggregation.
- Mapping through `AutoMapper`, CSV support with `CsvHelper` and `WebApiContrib.Core.Formatter.Csv`, and Excel export hooks via `OfficeOpenXml.Core.ExcelPackage`.
- Response caching, compression, and CSV media formatting to serve heavy exports efficiently.
- Dependency injection managed through `Microsoft.Extensions.DependencyInjection` using the wiring in `NativeDotNetInjector`.
- File attachment storage via the `IStorage` contract, implemented by `FileStorage` with a configurable `FileSystem` root path.

## Architecture & Methodology
- Layered solution with a Presentation API project, a rich Domain model plus metadata, Infrastructure.Data implementations, and CrossCutting configuration/IoC wiring.
- Domain-driven design models materials, orders, providers, transfers, direct exits, divergences, RMAs, installations, projects, profiles, and permissions across multiple aggregates.
- Repositories reuse a generic base while read-only query repositories leverage `FilteredQuery` and `PaginatedQuery` helpers for filtering and pagination.
- AutoMapper-backed `ViewModels` plus the `BaseController` helper standardize DTO creation so controllers stay thin.
- `SgmDataContext` centralizes persistence by applying Fluent mappings from `Infraestructure.Data/DatabaseMapping`, handling soft deletes/timestamps, and seeding the current user and installation context before each commit.
- Cross-cutting configuration objects (`JwtTokenConfiguration`, `FileSystem`, `LdapConfig`) are loaded from `appsettings.*.json`, which keeps the same binary adaptable in dev, staging, and production.

## Business Capabilities
- Authentication & roles: LDAP-backed login, JWT authentication, and authorization tokens enriched with installation, profile, and permission claims.
- Receivements: Controllers cover invoice-backed, transfer, third-party, and devolution receivements, with attachment downloads and streaming.
- Catalog & stock: Material, provider, project, installation, order, and branch-office controllers expose catalog data and stock visibility.
- Returns & divergences: RMA, divergence, direct exit, and custody flows capture logistics incidents, status changes, email steps, and attachments.
- Transfers: Internal installation transfers, direct exit transfers, and attendance material endpoints expose historical movement data.
- Synchronization: Database-driven sync commands are validated and executed safely through `SynchronizationService`.
- File handling: Attachments for invoices, photos, and divergence documents are persisted via the shared `IStorage` implementation.

## Security & Access Control
- JWT validation extends beyond the bearer scheme: `ValidateJwtTokenFilter`, `ValidateJwtTokenAttribute`, and `ValidateJwtActionTokenAttribute` ensure the right token and permission land on sensitive actions.
- Authorization policies are generated from the `ActionPermissions` enum so each controller action can declare the permission it requires.
- `GlobalMiddleware` loads user/installation claims into `SgmDataContext`, validates the optional `Installation` header against JWT claims, and converts domain exceptions into consistent HTTP responses.
- `ActionAuthorizeAttribute` plus `JwtTokenGeneratorService` allow admins and scoped profiles to receive the correct claims (permissions, installations, admin flag) for their workflows.

## Observability & Reliability
- Swagger is exposed at the application root, includes XML docs, and accepts the `Authorization` header via the custom swagger operation filter.
- `Serilog` (configured in `appsettings.json`) writes separate business and error logs with daily rollovers and can plug into Sentry for cross-service alerting.
- Response caching plus compression harmonize with CSV/Excel formatters so large exports stay snappy.
- Global exception middleware and filters ensure consistent JSON payloads for not-found, bad request, and authorization failures before controllers run.
- The solution ships with `ResponseCaching` and CSV-friendly formatters to handle data-heavy scenarios.

## Data Handling & Storage
- `SgmDataContext` applies Fluent Entity mappings, enforces timestamp and soft-delete semantics, and injects the current user and installation before saving changes.
- Repository/query pairings keep DDD boundaries clean: commands go through repositories, while specialized read-only classes (e.g., `RMAReadOnlyRepository`, `InstallationsReadOnlyRepository`) serve analytics and dropdowns.
- CSV/Excel exporters let downstream partners consume register-level data without depending on web UI components.
- File attachment storage validates filenames, creates directories on demand, and exposes streaming helpers through the `IStorage` interface.

## Testing & Extensibility
- `SBEISK.SGM.Tests.Domain` is wired up for domain-focused coverage and can expand to validate critical business logic without the API surface.
- IoC wiring via `NativeDotNetInjector.RegisterServices` makes it easy to swap implementations (for example, swap `FileStorage` out for a cloud provider).
- AutoMapper profiles, JWT helpers, and filter attributes provide consistent cross-cut behavior, so new features follow a predictable pattern across entity -> repository -> service -> view model -> controller.
- Environment-aware `Program.cs` configuration plus feature toggles in `appsettings.*.json` ensure smooth deployments across dev, staging, and production environments.
