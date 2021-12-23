//
//  CashView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 11/12/2021.
//

import SwiftUI

struct CashView: View {
	@Environment(\.colorScheme) var colorScheme
	private let id: Int
	private let image: Image
	private let name: String
	private let type: Money
	@State var ammount: String = ""
	@State var isLoading = false
	
	var body: some View {
		VStack(alignment: .leading) {
			HStack {
				Text(name)
					.font(.largeTitle)
					.bold()
				Spacer()
			}
			if !isLoading {
				Text(ammount)
					.font(.title)
					.padding([.leading, .top])
			} else {
				ProgressView()
					.progressViewStyle(.circular)
					.padding([.leading, .top])
				
			}
			Spacer()
		}
		.padding()
		.frame(maxWidth: .infinity)
		.frame(height: 160)
		.background(alignment: .bottomTrailing) {imageColor}
		.background(.regularMaterial)
		.mask(RoundedRectangle(cornerRadius: 8))
		.shadow(radius: 16)
		.task {
			isLoading = true
			do {
				let response = try await getCash(from: type)
				ammount = "\(response)"
			} catch let error  {
				print(error.localizedDescription)
				ammount = "Nie udało się przechwycić danych"
			}
			isLoading = false
		}
	}
	
	@ViewBuilder
	private var imageColor: some View {
		if colorScheme == .dark {
			image
				.resizable()
				.scaledToFit()
				.colorInvert()
				.frame(height: 100)
				.padding([.trailing, .bottom])
		} else {
			image
				.resizable()
				.scaledToFit()
				.frame(height: 100)
				.padding([.trailing, .bottom])
		}
		
	}
}

extension CashView {
	init(budgetType: BudgetType) {
		id = budgetType.id
		image = Image(budgetType.type.lowercased())
		name = budgetType.type
		type = Money(rawValue: budgetType.type) ?? .Budget
	}
}

struct CashView_Previews: PreviewProvider {
	static var previews: some View {
		CashView(budgetType: BudgetType.budgetTypeMock)
	}
}
