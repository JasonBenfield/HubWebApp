import { NumericValue } from '../../Shared/NumericValue';
import { NumericValues } from '../../Shared/NumericValues';

export class AppTypes extends NumericValues<AppType> {
	constructor(
		public readonly NotFound: AppType,
		public readonly WebApp: AppType,
		public readonly Service: AppType,
		public readonly Package: AppType,
		public readonly ConsoleApp: AppType
	) {
		super([NotFound,WebApp,Service,Package,ConsoleApp]);
	}
}

export class AppType extends NumericValue implements IAppType {
	public static readonly values = new AppTypes(
		new AppType(0, 'Not Found'),
		new AppType(10, 'Web App'),
		new AppType(15, 'Service'),
		new AppType(20, 'Package'),
		new AppType(25, 'Console App')
	);

	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}