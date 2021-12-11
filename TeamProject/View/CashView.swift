//
//  CashView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 11/12/2021.
//

import SwiftUI

struct CashView: View {
	private let image: Image
	private let name: String
	private let ammount: String
	
	var body: some View {
		VStack(alignment: .leading) {
			HStack {
				Text(name)
					.font(.largeTitle)
					.bold()
				Spacer()
			}
			Text(ammount)
				.font(.title)
				.padding([.leading, .top])
			Spacer()
		}
		.padding()
		.frame(maxWidth: .infinity)
		.frame(height: 160)
		.background(alignment: .bottomTrailing) {
			image.resizable().scaledToFit()
		}
		.background(.regularMaterial)
		.mask(RoundedRectangle(cornerRadius: 8))
		.shadow(radius: 16)
	}
}

extension CashView {
	init(budgetType: BudgetType) {
		image = Image(budgetType.type.lowercased())
		name = budgetType.type
		ammount = "1345.00"
	}
}

struct CashView_Previews: PreviewProvider {
	static var previews: some View {
		CashView(budgetType: BudgetType.budgetTypeMock)
	}
}
