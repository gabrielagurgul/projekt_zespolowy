//
//  CategoryViewModel.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import Foundation
import SwiftUI

class CategoryViewModel: ObservableObject {
	@Published var categoryName: String = ""
	@Published var categoryImage: Image = Image(systemName: "questionmark")
}
